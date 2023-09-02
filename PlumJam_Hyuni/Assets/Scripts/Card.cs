using UnityEngine;
using System;

[Serializable]
public class Card
{
    public string Name;
    public string Description;

    public string image;

    public int HP; //��� ���� Ƚ��
    public int current_HP;
    public int Ability; //���� ���̵�, �л� �ɷ�

    public Card(string n, bool isAssign = false) {
        this.Name = n;
        if (isAssign) {
            Lesson info = LessonDataLoader.instance.GetLessonData(n);
            Description = info.des;
            image = info.type;
            HP = 1;
            current_HP = 1;
            Ability = info.abili;
        }
        else {
            Student info = StudentDataLoader.instance.GetStudentData(n);
            Description = info.des;
            image = info.code;
            HP = info.hp;
            current_HP = info.hp;
            Ability = info.abili;
        }
        
        //Debug.Log($"������ ������ : {n} {Description} {image} {HP} {Ability}");
    }
}
