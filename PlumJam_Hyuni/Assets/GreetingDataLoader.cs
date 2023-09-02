using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using System;
[Serializable]
public class Greeting {
    public string prof;
    public string type;
    public string talk;
    public Greeting(string n, string c, string d) {
        prof = n;
        type = c;
        talk = d;
    }
}
public class GreetingDataLoader : MonoBehaviour
{
    public static GreetingDataLoader instance;

    [SerializeField]
    public List<Greeting> Data_Greeting= new List<Greeting>();

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }

    void Start() {
        TextAsset data = (TextAsset)Resources.Load("Data_Greeting");
        string[] s = data.text.Substring(0, data.text.Length - 1).Split('\n');

        for (int i = 0; i < s.Length; i++) {
            if (i == 0) continue;
            string[] value = s[i].Split(',');
            Data_Greeting.Add(new Greeting(value[0], value[1], value[2]));
            //print($"대화 데이터 저장 : {value[0]}");
        }
    }

    public Greeting GetGreetingData(string name = null, string type = null) {
        if (name != null && type != null) {
            var target = Data_Greeting.Where(x => x.prof.Equals(name) && x.type.Equals(type));
            var t = target.ToArray();
            return t[(int)Random.Range(0, t.Count() - 1)];
        }
        Debug.LogWarning($"대화 정보 가져오기 실패 : {name}");
        return null;
    }
}
