using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundMangaer _sound = new SoundMangaer();
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource;} }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundMangaer Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }


    void Start()
    {
        Init();
    }

    void Update()
    {
        // Check 하는 부분이 유일하게 된다. 
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Managers");
        if (s_instance == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();
        }

        DontDestroyOnLoad(go);
        s_instance = go.GetComponent<Managers>();

        // Instance를 이용하면 무한 루프
        s_instance._pool.Init();
        s_instance._sound.Init();
    }

    // Scene 이동할 때 날려야하는것들을 여기서 다 날린다.
    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();

        // 다른 곳에서 Pooling된 object를 사용할 수 있으므로 마지막에
        Pool.Clear();
    }
}
