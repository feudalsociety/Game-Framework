using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }

    DataManager _data = new DataManager();
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundMangaer _sound = new SoundMangaer();
    UIManager _ui = new UIManager();


    public static DataManager Data { get { return Instance._data; } }
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
        // Check �ϴ� �κ��� �����ϰ� �ȴ�. 
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Managers");
        if (s_instance == null)
        {
            go = new GameObject { name = "@Managers" };
            go.AddComponent<Managers>();

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            // Instance�� �̿��ϸ� ���� ����
            s_instance._data.Init(); // _data�� ��κ��� ��Ȳ���� �׻� ��� �־���ϱ� ������. clear ���� ��
            s_instance._pool.Init();
            s_instance._sound.Init();
        }
    }

    // Scene �̵��� �� �������ϴ°͵��� ���⼭ �� ������.
    public static void Clear()
    {
        Sound.Clear();
        Input.Clear();
        Scene.Clear();
        UI.Clear();

        // �ٸ� ������ Pooling�� object�� ����� �� �����Ƿ� ��������
        Pool.Clear();
    }
}
