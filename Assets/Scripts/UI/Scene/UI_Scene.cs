using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    public virtual void Init()
    {
        // scene�� sorting �ʿ���� sortihg order �ڵ����� 0���� ������
        Managers.UI.SetCanvus(gameObject, false);
    }
}
