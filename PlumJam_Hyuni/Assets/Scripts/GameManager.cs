using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System;

public enum Week { ��, ȭ, ��, ��, ��, �ָ�};

public class GameManager : MonoBehaviour
{
    public Transform vp_Week;
    public GameObject pn_ClassInfo;
    Week e_week;
    int week = 1;
    int maxweek = 14;

    void Start()
    {
        Debug.LogWarning("���� ������ �ҷ����� �̱���");
        Clear_Week();
        WeekSchedule();
    }

    void Clear_Week() {
        for(int i = 0; i < vp_Week.childCount; i++) {
            Destroy(vp_Week.GetChild(i).gameObject);
        }
    }

    void WeekSchedule(int level = 1) {
        GameObject day = PrefabContainer.instance.WeekDay;
        GameObject end = PrefabContainer.instance.WeekEnd;
        int nextconcert = (int)Random.Range(0, 3) + 1;
        print($"���� �������� {nextconcert}��");
        for(int i = 0; i < nextconcert; i++) {
            for(int d = 0; d < Enum.GetNames(typeof(Week)).Length; d++) {
                MakeDay((Week)Enum.ToObject(typeof(Week), d));
            }
        }
        Debug.LogWarning("������ �ָ��� �ܼ�Ʈ�� ���� �̱���");
    }

    void MakeDay(Week week) {
        GameObject day = Instantiate(PrefabContainer.instance.WeekDay, vp_Week);
        day.GetComponent<Day>().Set(week);

        if (week == Week.�ָ�) {
            MakeRest(day);
        }
        else {
            int count = (int)Random.Range(0, 3) + 1;
            for (int i = 0; i < count; i++) {
                MakeClass(day);
            }
        }
    }

    void MakeClass(GameObject day) {
        Button btn = Instantiate(PrefabContainer.instance.btn_Class, day.GetComponent<Day>().pn_Classess);
        cProfessor c = ProfessorDataLoader.instance.GetProfessorData();
        btn.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite(c.code);
        btn.GetComponent<ClassInfo>().info = ClassDataLoader.instance.GetClassData(c.name);
        btn.GetComponent<ClassInfo>().sprite = ImageLoader.instance.GetSprite(c.code);
        //print($"{btn.GetComponent<ClassInfo>().info.name} : {btn.GetComponent<ClassInfo>().info.prof}");
        btn.onClick.AddListener(() => ShowClassDetail(btn.GetComponent<ClassInfo>()));
    }

    void MakeRest(GameObject day) {
        Button btn = Instantiate(PrefabContainer.instance.btn_Class, day.GetComponent<Day>().pn_Classess);
        btn.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite("rest");
        btn.GetComponent<ClassInfo>().info.name = "�ָ��� ���� ��";
        btn.GetComponent<ClassInfo>().info.prof = "";
        btn.GetComponent<ClassInfo>().info.des= "������ �� ������ �޽Ľð�";
        btn.GetComponent<ClassInfo>().sprite = ImageLoader.instance.GetSprite("rest");
        btn.onClick.AddListener(() => ShowClassDetail(btn.GetComponent<ClassInfo>()));
    }

    void ShowClassDetail(ClassInfo info) {
        pn_ClassInfo.SetActive(true);
        pn_ClassInfo.GetComponent<pn_classinfo>().img_prof.sprite = info.sprite;
        pn_ClassInfo.GetComponent<pn_classinfo>().txt_prof.SetText(info.info.prof);
        pn_ClassInfo.GetComponent<pn_classinfo>().txt_des.SetText(info.info.des);
        pn_ClassInfo.GetComponent<pn_classinfo>().txt_name.SetText(info.info.name);
    }

    void ClearClass(Transform t) {
        Debug.LogWarning($"���� �Ϸ� : ");
        Instantiate(PrefabContainer.instance.img_Clear, t);
    }
}
