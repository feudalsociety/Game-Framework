using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// scene은 하나밖에 없는거라고 가정했고, 독립적으로 뜨는 팝업도 아니기 때문에
public class UI_Inven_Item : UI_Base
{
    // test할겸 개수도 적으니까 합쳐서 gameobject로 들고 있어보자
    enum GameObjects
    {
        ItemIcon,
        ItemNameText,
    }

    string _name;

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        Get<GameObject>((int)GameObjects.ItemIcon).AddUIEvent((PointerEventData) => { Debug.Log($"item click {_name}"); });
    }

    public void SetInfo(string name)
    {
        _name = name;
    }
}
