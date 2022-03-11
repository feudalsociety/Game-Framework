using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Canvus에 있는 Sortorder을 관리해주기 위함
public class UIManager
{
    // 최근에 사용한 UI의 order
    int _order = 10; // 안에서 사용하는 UI_Popup끼리의 order

    // 가장 마지막에 띄운 것이 먼저 삭제 - stack 구조
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            
            return root;
        }
    }

    // 외부에서 Popup이 켜질 때 UIManger에서 Canvus order을 지정함
    public void SetCanvus(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // 중첩이 된 canvas라도 부모의 sorting order과 상관없이 자신의 sorting order을 가지겠다
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // Poupup이랑 연관 없는 UI
        {
            canvas.sortingOrder = 0;
        }
    }

    public T MakeSubItem<T>(Transform parent = null, string name = null) where T : UI_Base
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if (parent != null)
            go.transform.SetParent(parent);

        return go.GetOrAddComponent<T>();
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name)) name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");
        // 해당 component가 없으면 추가후 get
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        // 부모 지정
        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    // name은 prefab의 이름, T로 script를 건내줄거임, 
    // script 이름과 prefab의 이름을 맞춰줄 것이지만 이름을 명시하지 않으면 T를 그대로 사용
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        // 넣어준 type의 이름 추출
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
            Debug.Log($"{name}");
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        // 해당 component가 없으면 추가후 get
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);
        // _order++;  // Scene에 다가 drag&drop해서 상성한 UI가 처리안됨
        // UI_Popup이 start될 때 넣어줄 것

        // 부모 지정
        go.transform.SetParent(Root.transform);

        return popup;
    }

    // 내가 누군지 명시해서 실제로 삭제되는 애가 맞는지 test
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if(_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        // 가장 최근에 띄운 Popup
        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void closeAllPopupUI()
    {
        while(_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        closeAllPopupUI();
        _sceneUI = null;
    }
}
