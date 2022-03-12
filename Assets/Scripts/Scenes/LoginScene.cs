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
            // 비동기도 지원함, GameScene이 규모가 큰 경우
            // 넘어가는 동안 resource들을 조금씩 로드하는 구현 : 로딩화면
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }

    public override void Clear()
    {
        Debug.Log("Login Scene Clear");
    }
}
