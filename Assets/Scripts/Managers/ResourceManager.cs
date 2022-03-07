using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{ 
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    
    public  GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if(prefab == null)
        {
            Debug.Log($"Falied to Load prefab : {path}");
            return null;
        }

        // Object를 붙여준 이유는 재귀적 호출을 막기위해
        return Object.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        Object.Destroy(go);
    }

}
