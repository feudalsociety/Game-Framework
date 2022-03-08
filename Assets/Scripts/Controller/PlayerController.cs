using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    Vector3 _destPos;

    // Start is called before the first frame update
    void Start()
    {
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    public enum PlayerState
    { 
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {

    }

    void UpdateMoving()
    {
        // dest 방향으로 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            // 이동, 속도가 너무 빨라서 목표지점을 넘어서는 것을 방지
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0.0f, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
            // transform.LookAt(_destPos);
        }

        // 애니매이션
        Animator anim = GetComponent<Animator>();
        // 현재 게임상태에 대한 정보를 넘겨준다.
        anim.SetFloat("speed", _speed);
    }

    void UpdateIdle()
    {
        // 애니매이션
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    void OnRunEvent(int a)
    {
        Debug.Log($"뚜벅 뚜벅 {a}");
    }

    // Update is called once per frame
    void Update()
    {
        // 단점 동시에 두가지 상태를 가질 수는 없고 한번에 하나씩의 상태만 갖는다고 가정하면
        switch (_state)
        { 
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    // evt로 input 상태가 넘어감
    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (_state == PlayerState.Die) return;

        // 주석 처리하면 마우스 누른 상태에서도 이동
        // if (evt != Define.MouseEvent.Click) return;

        // 더 사용하기 쉬운 버전
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); 

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
            // Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}
