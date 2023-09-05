using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using System;

[Serializable]
public class Lesson {
    public string prof;
    public string type;
    public string des;
    public int abili;
    public Lesson(string p, string t, string d, int a) {
        prof = p;
        type = t;
        des = d;
        abili = a;
    }
}
public class LessonDataLoader : MonoBehaviour
{
    public static LessonDataLoader instance;

    [SerializeField]
    public List<Lesson> Data_Lesson = new List<Lesson>();

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }

    void Start() {
        TextAsset data = (TextAsset)Resources.Load("Data_Lesson");
        string[] s = data.text.Substring(0, data.text.Length - 1).Split('\n');

        for (int i = 0; i < s.Length; i++) {
            if (i == 0) continue;
            string[] value = s[i].Split(',');
            Data_Lesson.Add(new Lesson(value[0], value[1], value[2], int.Parse(value[3])));
            //print($"수업 데이터 저장 : {value[0]}");
        }
    }

    public Lesson GetLessonData(string prof) {
        if (prof != null) {
            var target = Data_Lesson.Where(x => x.prof.Equals(prof));
            var t = target.ToArray();
            return t[(int)Random.Range(0, t.Count() - 1)];
        }
        Debug.LogWarning($"수업 정보 가져오기 실패 : {prof}");
        return null;
    }
}
