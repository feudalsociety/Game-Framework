using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    // 입력이 있으면 Event로 전파
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    bool _pressed = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // 누가 event를 받았는지 확인 할 수 있다. 마우스를 때는것도 상태 변화로 인식할 수 있으므로
        // anyKey만 따로 검사하지 않는다.
        // if (Input.anyKey) return; // any key or mousebutton held down?

        if(Input.anyKey && KeyAction != null) KeyAction.Invoke();

        if(MouseAction != null)
        {
            if(Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
                // 0.2초 이상 마우스를 누르고 있으면 Drag로 인식 한다던가 할 수 있겠다.
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
