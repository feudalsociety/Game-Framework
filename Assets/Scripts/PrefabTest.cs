using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    // public GameObject prefab;

    GameObject tank;
    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank");
        // prefab = Resources.Load<GameObject>("Prefabs/Tank");
        // GameObject tank = Instantiate(prefab);

        // Managers.Resource.Destroy(tank);
        Destroy(tank, 3.0f); // 3초후에 gameObject 삭제
    }
}
