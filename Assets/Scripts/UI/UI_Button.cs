using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    // UI ����� ���⼭ �߰�
    enum Buttons
    {
        PointButton
    }

    enum Texts
    { 
        PointText,
        ScoreText
    }

    // ��� enum�� GameObject�� ���ĵ� ��
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
        // reflection�� �̿��Ͽ� enum�� �Ѱ��ش�.
        // �̸��� <T> component�� ���� �ִ� object�� ã�� ���� ��ǥ
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        // findchild���� gameobject�� component�� �ƴϹǷ� ã�� �� ����.
        Bind<GameObject>(typeof(GameObjects)); 
        Bind<Image>(typeof(Images));

        // default type = Define.UIEvent.Click
        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        // �̰��� callback�� go�� ���Ƿ� 2�ܰ�� ����
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
