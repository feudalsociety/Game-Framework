using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Canvus�� �ִ� Sortorder�� �������ֱ� ����
public class UIManager
{
    // �ֱٿ� ����� UI�� order
    int _order = 10; // �ȿ��� ����ϴ� UI_Popup������ order

    // ���� �������� ��� ���� ���� ���� - stack ����
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

    // �ܺο��� Popup�� ���� �� UIManger���� Canvus order�� ������
    public void SetCanvus(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        // ��ø�� �� canvas�� �θ��� sorting order�� ������� �ڽ��� sorting order�� �����ڴ�
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // Poupup�̶� ���� ���� UI
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
        // �ش� component�� ������ �߰��� get
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        // �θ� ����
        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    // name�� prefab�� �̸�, T�� script�� �ǳ��ٰ���, 
    // script �̸��� prefab�� �̸��� ������ �������� �̸��� ������� ������ T�� �״�� ���
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
    {
        // �־��� type�� �̸� ����
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
            Debug.Log($"{name}");
        }

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");
        // �ش� component�� ������ �߰��� get
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);
        // _order++;  // Scene�� �ٰ� drag&drop�ؼ� ���� UI�� ó���ȵ�
        // UI_Popup�� start�� �� �־��� ��

        // �θ� ����
        go.transform.SetParent(Root.transform);

        return popup;
    }

    // ���� ������ ����ؼ� ������ �����Ǵ� �ְ� �´��� test
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

        // ���� �ֱٿ� ��� Popup
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
