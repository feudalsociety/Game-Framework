using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Stat
// public으로 해야함 굳이 private으로 하고 싶다면
// field 위에 [SerializeField]를 붙여야함, data 이름과 데이터형도 맞추어야함
[Serializable]
public class Stat
{
    public int level;
    public int hp;
    public int attack;
}

[Serializable]
public class StatData : ILoader<int, Stat>
{
    public List<Stat> stats = new List<Stat>();

    public Dictionary<int, Stat> MakeDict()
    {
        Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
        foreach (Stat stat in stats)
            dict.Add(stat.level, stat);
        // Valiate하고 싶으면 여기서 코드 추가
        return dict;
    }
}
#endregion