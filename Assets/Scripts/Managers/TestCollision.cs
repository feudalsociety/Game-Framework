using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision @ {collision.gameObject.name}");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger @ {other.gameObject.name}");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // pixel ��ǥ = screen ��ǥ
        // Debug.Log(Input.mousePosition);

        // Camera.main.ScreenToViewportPoint(Input.mousePosition); // viewport 0~1, ȭ�� ����


        if (Input.GetMouseButtonDown(0))
        {
            // �� ����ϱ� ���� ����
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); // 1�� ���ȸ� ����

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");

            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    // mouse�� world position. ���̷� nearClipPlane
        //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        //    Vector3 dir = mousePos - Camera.main.transform.position;
        //    dir = dir.normalized;

        //    Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f); // 1�� ���ȸ� ����

        //    RaycastHit hit;
        //    if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
        //    {
        //        Debug.Log($"Raycast Camera @ {hit.collider.gameObject.name}");
        //    }
        //}
    }
}
