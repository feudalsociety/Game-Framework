using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    float _yAngle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        _yAngle += Time.deltaTime * _speed;
        // 절대 회전값
        // transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);

        // +- delta, 옵션으로 world 기준, local 기준 선택할 수 있다.
        // transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));
        // transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        if (Input.GetKey(KeyCode.W))
        {
            // word 좌표계 기준
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.1f);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
            // transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            // transform.position += transform.TransformDirection(Vector3.forward * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.1f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
            // transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            // transform.Translate(Vector3.back * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.1f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
            // transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            // transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.1f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
            // transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            // transform.Translate(Vector3.right * Time.deltaTime * _speed);
        }
    }
}
