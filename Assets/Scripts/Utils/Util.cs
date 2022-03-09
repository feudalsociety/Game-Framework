using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util 
{
    public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        // handler�� ���ٸ� �߰��� ���ش�.
        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    // gameobject �޴� ����
    public static GameObject findChild(GameObject go, string name = null, bool recursive = false)
    {
        // ��� Gameobject�� transform component�� ������ �����Ƿ�
        Transform transform = findChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }



    // go : �ֻ��� �θ� object, name = �̸� �̸��� �Է����� ������ 
    // type���� �ش��ϸ� return
    // T�� ã�� ���� component 
    public static T findChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if ( go == null) return null;

        if(recursive == false)
        {
            // �ٷ� �ؿ� �ִ� child �ϳ��� scan
            for(int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    // T compnent�� ������ ���� line������ null�� return�Ѵ�.
                    T component = transform.GetComponent<T>();
                    if(component != null) return component;
                }

            }
        }
        else
        {
            // recursive�ϰ� ���� child ��� scan
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                // null�̸� ù��°�� ã���� return
                if(string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

}
