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

    public Card(string n) {
        this.Name = n;
        Student info = StudentDataLoader.instance.GetStudentData(n);
        Description = info.des;
        image = info.code;
        HP = info.hp;
        current_HP = info.hp;
        Ability = info.abili;
        Debug.Log($"������ ������ : {n} {Description} {image} {HP} {Ability}");
    }
}
