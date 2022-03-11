using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    // �Է��� ������ Event�� ����
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    bool _pressed = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // ���� event�� �޾Ҵ��� Ȯ�� �� �� �ִ�. ���콺�� ���°͵� ���� ��ȭ�� �ν��� �� �����Ƿ�
        // anyKey�� ���� �˻����� �ʴ´�.
        // if (Input.anyKey) return; // any key or mousebutton held down?

        if(Input.anyKey && KeyAction != null) KeyAction.Invoke();

        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
                // 0.2�� �̻� ���콺�� ������ ������ Drag�� �ν� �Ѵٴ��� �� �� �ְڴ�.
            }
            else
            {
                if(_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);
                _pressed = false;
            }
        }
    }

    public void Clear()
    {
        KeyAction = null; 
        MouseAction = null;
    }
}
