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

        if(KeyAction != null) KeyAction.Invoke();
    }
}
