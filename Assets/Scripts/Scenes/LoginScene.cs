using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Login;

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 2; i++)
            Managers.Resource.Instantiate("UnityChan");

        foreach(GameObject obj in list)
        {
            Managers.Resource.Destroy(obj);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            // �񵿱⵵ ������, GameScene�� �Ը� ū ���
            // �Ѿ�� ���� resource���� ���ݾ� �ε��ϴ� ���� : �ε�ȭ��
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }
}
