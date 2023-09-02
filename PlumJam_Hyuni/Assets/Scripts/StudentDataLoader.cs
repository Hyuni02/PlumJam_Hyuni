using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Student {
    public string name;
    public string code;
    public string des;
    public int hp;
    public int abili;
    public int grade;
    public Student(string n, string c, string d, int h, int a, int g) {
        name = n;
        code = c;
        des = d;
        hp = h;
        abili = a;
        grade = g;
    }
}

public class StudentDataLoader : MonoBehaviour
{
    public static StudentDataLoader instance;
    [SerializeField]
    public List<Student> Data_Student = new List<Student>();

    private void Awake() {
        if(instance != null) {
            Destroy(this);
        }
        instance = this;
    }

    void Start()
    {
        TextAsset data = (TextAsset)Resources.Load("Data_Student");
        string[] s = data.text.Substring(0, data.text.Length - 1).Split('\n');

        for(int i=0;i<s.Length;i++) {
            if (i == 0) continue;
            string[] value = s[i].Split(',');
            Data_Student.Add(new Student(value[0], value[1], value[2], int.Parse(value[3]), int.Parse(value[4]), int.Parse(value[5])));
        }
    }

    public Student GetStudentData(string name) {
        foreach(var st in Data_Student) {
            if (st.name == name) return st;
        }
        Debug.LogWarning($"학생 정보 가져오기 실패 : {name}");
        return null;
    }
    
}
