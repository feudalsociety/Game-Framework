using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Resource Manager 보조
public class PoolManager
{
    #region Pool
    class Pool
    { 
        public GameObject Original { get; private set; }
        public Transform Root { get; set; } // UnityChan Root 산하에 위치 시키기 위해서

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
            // 원본을 받고 복사본을 만든다.
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

        // Original은 굳이 안받아도됨. 아래 interface에서 받음
        public Poolable Pop(Transform parent)
        {
            Poolable poolable;

            if (_poolStack.Count > 0)
                poolable = _poolStack.Pop();
            else
                poolable = Create();

            // DontdestroyOnLoad에서 Pool Init으로 생성되었을 경우, 해제 용도
            // 처음 Pop할 때 @Scene산하로 이동, Scene 뿐만 아니라 다른 무엇이든 
            // DontdestroyOnLoad가 아닌 Object 밑으로 한번이라도 이동하면 됨
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

    // Transform으로 가지고 있어도되고 GameObject, 큰 차이 없음
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
        pool.Root.parent = _root; // @Pool_Root 와 연결, Don't Destroy On Load 부분

        _pool.Add(original.name, pool);
    }

    // Push, Pop 용어를 많이 씀
    // 반환, 아무리 Poolable이라도 다른 Gameobject를 같은 Pool에 넣으면 안됨
    public void Push(Poolable poolable)
    {
        string name = poolable.gameObject.name;
        // pool이 없는 상태에서 Push 한다면?
        if (_pool.ContainsKey(name) == false)
        {
            GameObject.Destroy(poolable.gameObject);
            return;
        }
        _pool[name].Push(poolable);
    }

    // original 이름으로 pool을 찾아서 거기서 다시 Pop
    // 개개인 마다 class를 나눠서 관리를 하면 구현이 쉬울 것이다.
    public Poolable Pop(GameObject original,  Transform parent = null)
    {
        if (_pool.ContainsKey(original.name) == false)
            CreatePool(original);

        return _pool[original.name].Pop(parent);
    }

    // Resources Load를 이용해서 원본을 찾기보다, 혹시라도 PoolManager에 원본을 들고 있다면
    // 그것을 바로 반환. 원본을 여러번 찾을 필요없이 한번 찾은 Original은 바로 사용
    public GameObject GetOriginal(string name)
    {
        if (_pool.ContainsKey(name) == false)
            return null;
        return _pool[name].Original;
    }

    // Caching을 유지할지는 game마다 다르다.
    public void Clear()
    {
        foreach (Transform child in _root)
            GameObject.Destroy(child.gameObject);

        _pool.Clear();
    }
}
