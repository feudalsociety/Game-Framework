using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;

    bool _moveToDest = false;
    Vector3 _destPos;

    // Start is called before the first frame update
    void Start()
    {
        // �ι� ������ �ι� ȣ���. Ȥ�ö� ���� ��û������ 
        Managers.Input.KeyAction -= OnKeyBoard;
        // ���� ��û
        Managers.Input.KeyAction += OnKeyBoard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
    }

    float _yAngle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (_moveToDest) 
        {
            // dest �������� �̵�
            Vector3 dir = _destPos - transform.position;
            if(dir.magnitude < 0.0001f)
            {
                _moveToDest = false;
            }
            else
            {
                // �̵�, �ӵ��� �ʹ� ���� ��ǥ������ �Ѿ�� ���� ����
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0.0f, dir.magnitude);
                transform.position += dir.normalized * moveDist;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
                // transform.LookAt(_destPos);
            }
        }
    }

    void OnKeyBoard()
    {
        _yAngle += Time.deltaTime * _speed;

        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        _moveToDest = false;
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if (evt != Define.MouseEvent.Click) return;

        // �� ����ϱ� ���� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); 

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _moveToDest = true;
            // Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        }
    }
}