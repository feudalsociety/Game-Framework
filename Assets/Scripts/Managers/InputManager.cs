using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    // �Է��� ������ Event�� ����
    public Action KeyAction = null;

    public void OnUpdate()
    {
        if (Input.anyKey == false) return;

        // ���� event�� �޾Ҵ��� Ȯ�� �� �� �ִ�.
        if(KeyAction != null) KeyAction.Invoke();
    }
}
