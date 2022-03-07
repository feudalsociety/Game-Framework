using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CamerMode _mode = Define.CamerMode.QuarterView;
    [SerializeField]
    Vector3 _detla = new Vector3(0.0f, 6.0f, -5.0f);  // player 기준으로 얼마나 떨어져 있는지
    [SerializeField]
    GameObject _player = null;

    // Start is called before the first frame update
    void Start()    
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_mode == Define.CamerMode.QuarterView)
        {
            RaycastHit hit;
            // camera와 player 사이에 object가 있으면
            if (Physics.Raycast(_player.transform.position, _detla, out hit, _detla.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                transform.position = _player.transform.position + _detla.normalized * dist;
            }
            else 
            {
                transform.position = _player.transform.position + _detla;
            }
            transform.LookAt(_player.transform);
        }
    }

    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CamerMode.QuarterView;
        _detla = delta;
    }
}
