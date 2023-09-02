using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
using System.Linq;

[Serializable]
public class cClass {
    public string name;
    public string prof;
    public string des;
    public cClass(string n, string p, string d) {
        name = n;
        prof = p;
        des = d;
    }
}
public class ClassDataLoader : MonoBehaviour
{
    public static ClassDataLoader instance;

    [SerializeField]
    public List<cClass> Data_Class = new List<cClass>();

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }

    void Start() {
        TextAsset data = (TextAsset)Resources.Load("Data_Class");
        string[] s = data.text.Substring(0, data.text.Length - 1).Split('\n');

        for (int i = 0; i < s.Length; i++) {
            if (i == 0) continue;
            string[] value = s[i].Split(',');
            Data_Class.Add(new cClass(value[0], value[1], value[2]));
            //print($"수업 데이터 저장 : {value[0]} - {value[1]}");
        }
    }

    public cClass GetClassData(string prof, string name = null) {
        if(name == null && prof != null) {
            var target = Data_Class.Where(x => x.prof.Equals(prof));
            var t = target.ToArray();
            return t[(int)Random.Range(0, t.Count()-1)];
        }
        Debug.LogWarning($"수업 정보 가져오기 실패 : {prof}");
        return null;
    }
}
