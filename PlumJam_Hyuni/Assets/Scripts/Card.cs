using UnityEngine;
using System;

[Serializable]
public class Card
{
    public string Name;
    public string Description;

    public string image;

    public int HP; //사용 가능 횟수
    public int current_HP;
    public int Ability; //과제 난이도, 학생 능력

    public Card(string n) {
        this.Name = n;
        Student info = StudentDataLoader.instance.GetStudentData(n);
        Description = info.des;
        image = info.code;
        HP = info.hp;
        current_HP = info.hp;
        Ability = info.abili;
        Debug.Log($"데이터 가져옴 : {n} {Description} {image} {HP} {Ability}");
    }
}
