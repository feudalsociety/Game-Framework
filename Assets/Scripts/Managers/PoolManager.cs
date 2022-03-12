using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resource Manager ����
public class PoolManager
{
    #region Pool
    class Pool
    { 
        public GameObject Original { get; private set; }
        public Transform Root { get; set; } // UnityChan Root ���Ͽ� ��ġ ��Ű�� ���ؼ�

        Stack<Poolable> _poolStack = new Stack<Poolable>();

        public void Init(GameObject original, int count = 5)
        {
            Original = original;
            Root = new GameObject().transform;
            Root.name = $"{original.name}_Root";

            for(int i = 0; i < count; i++)
            {
                Push(Create());
            }
        }

        Poolable Create()
        {
            // ������ �ް� ���纻�� �����.
            GameObject go = Object.Instantiate<GameObject>(Original);
            go.name = Original.name;
            return go.GetOrAddComponent<Poolable>();
        }

        public void Push(Poolable poolable)
        {
            if (poolable == null)
                return;

            poolable.transform.parent = Root;
            poolable.gameObject.SetActive(false);
            poolable.IsUsing = false;

            _poolStack.Push(poolable);
        }

        // Original�� ���� �ȹ޾Ƶ���. �Ʒ� interface���� ����
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            // DontdestroyOnLoad���� Pool Init���� �����Ǿ��� ���, ���� �뵵
            // ó�� Pop�� �� @Scene���Ϸ� �̵�, Scene �Ӹ� �ƴ϶� �ٸ� �����̵� 
            // DontdestroyOnLoad�� �ƴ� Object ������ �ѹ��̶� �̵��ϸ� ��
            if (parent == null)
                poolable.transform.parent = Managers.Scene.CurrentScene.transform;

            poolable.gameObject.SetActive(true);
            poolable.transform.parent = parent;
            poolable.IsUsing = true;
            return poolable;
        }
    }
    #endregion

    Dictionary<string, Pool> _pool = new Dictionary<string, Pool>();

    // Transform���� ������ �־�ǰ� GameObject, ū ���� ����
    Transform _root;

    public void Init()
    {
        if(_root == null)
        {
            _root = new GameObject { name = "@Pool_Root" }.transform;
            Object.DontDestroyOnLoad(_root);
        }
    }

    public void CreatePool(GameObject original, int count = 5)
    {
        Pool pool = new Pool();
        pool.Init(original, count);
        pool.Root.parent = _root; // @Pool_Root �� ����, Don't Destroy On Load �κ�

        _pool.Add(original.name, pool);
    }

    // Push, Pop �� ���� ��
    // ��ȯ, �ƹ��� Poolable�̶� �ٸ� Gameobject�� ���� Pool�� ������ �ȵ�
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        // pool�� ���� ���¿��� Push �Ѵٸ�?
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        _pool[name].Push(poolable);
    }

    // original �̸����� pool�� ã�Ƽ� �ű⼭ �ٽ� Pop
    // ������ ���� class�� ������ ������ �ϸ� ������ ���� ���̴�.
    public Poolable Pop(GameObject original,  Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }

    // Resources Load�� �̿��ؼ� ������ ã�⺸��, Ȥ�ö� PoolManager�� ������ ��� �ִٸ�
    // �װ��� �ٷ� ��ȯ. ������ ������ ã�� �ʿ���� �ѹ� ã�� Original�� �ٷ� ���
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

    // Caching�� ���������� game���� �ٸ���.
    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
