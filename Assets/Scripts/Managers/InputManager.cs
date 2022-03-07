using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    // 입력이 있으면 Event로 전파
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false) return;

        // 누가 event를 받았는지 확인 할 수 있다.
        if(KeyAction != null) KeyAction.Invoke();
    }
}
