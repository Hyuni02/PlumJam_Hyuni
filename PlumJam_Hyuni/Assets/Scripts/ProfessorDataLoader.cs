using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using System;
[Serializable]
public class cProfessor {
    public string name;
    public string code;
    public string des;
    public string classes;
    public cProfessor(string n, string c, string d, string cs) {
        name = n;
        code = c;
        des = d;
        classes = cs;
    }
}
public class ProfessorDataLoader : MonoBehaviour {
    public static ProfessorDataLoader instance;

    [SerializeField]
    public List<cProfessor> Data_Professor = new List<cProfessor>();

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }

    void Start() {
        TextAsset data = (TextAsset)Resources.Load("Data_Professor");
        string[] s = data.text.Substring(0, data.text.Length - 1).Split('\n');

        for (int i = 0; i < s.Length; i++) {
            if (i == 0) continue;
            string[] value = s[i].Split(',');
            Data_Professor.Add(new cProfessor(value[0], value[1], value[2], value[3]));
            //print($"교수 데이터 저장 : {value[0]}");
        }
    }

    public cProfessor GetProfessorData(string name = null) {
        if (name == null) {
            return Data_Professor[(int)Random.Range(0, Data_Professor.Count)];
        }

        foreach (var st in Data_Professor) {
            if (st.name == name) return st;
        }
        Debug.LogWarning($"교수 정보 가져오기 실패 : {name}");
        return null;
    }
}
