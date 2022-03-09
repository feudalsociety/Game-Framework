using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    // UI 목록을 여기서 추가
    enum Buttons
    {
        PointButton
    }

    enum Texts
    { 
        PointText,
        ScoreText
    }

    // 모든 enum을 GameObject로 합쳐도 됨
    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon
    }

    private void Start()
    {
        // reflection을 이용하여 enum을 넘겨준다.
        // 이름과 <T> component를 갖고 있는 object를 찾는 것이 목표
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        // findchild에서 gameobject는 component가 아니므로 찾을 수 없다.
        Bind<GameObject>(typeof(GameObjects)); 
        Bind<Image>(typeof(Images));

        // default type = Define.UIEvent.Click
        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        // 이경우는 callback에 go가 들어가므로 2단계로 나눔
        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }

    int _score = 0;

    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"score : {_score}";
    }
}
