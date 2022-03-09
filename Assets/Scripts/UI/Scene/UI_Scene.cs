using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Scene : UI_Base
{
    public virtual void Init()
    {
        // scene은 sorting 필요없음 sortihg order 자동으로 0으로 설정됨
        Managers.UI.SetCanvus(gameObject, false);
    }
}
