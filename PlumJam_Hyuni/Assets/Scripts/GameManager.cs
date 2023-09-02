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

    public GameObject selectedClass;

    public GameObject Canvas_Schedule;
    public GameObject Canvas_Class;

    Week e_week;
    //int week = 1;
    //int maxweek = 14;
    int level = 1;
    int phase = 0;
    List<List<Lesson>> lst_lessons;

    void Start()
    {
        Debug.LogWarning("���� ������ �ҷ����� �̱���");
        Clear_Week();
        WeekSchedule();
        Canvas_Schedule.SetActive(true);
        Canvas_Class.SetActive(false);
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
        //Debug.LogWarning("������ �ָ��� �ܼ�Ʈ�� ���� �̱���");
        MakeConcert();

        EmphasizeToday();
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
        btn.onClick.AddListener(() => SetSelectedClass(btn.transform));
    }

    void MakeRest(GameObject day) {
        Button btn = Instantiate(PrefabContainer.instance.btn_Class, day.GetComponent<Day>().pn_Classess);
        btn.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite("rest");
        btn.GetComponent<ClassInfo>().info.name = "�ָ��� ���� ��";
        btn.GetComponent<ClassInfo>().info.prof = "";
        btn.GetComponent<ClassInfo>().info.des= "������ �� ������ �޽Ľð�";
        btn.GetComponent<ClassInfo>().sprite = ImageLoader.instance.GetSprite("rest");
        btn.onClick.AddListener(() => ShowClassDetail(btn.GetComponent<ClassInfo>()));
        btn.onClick.AddListener(() => SetSelectedClass(btn.transform));
    }

    void MakeConcert() {
        GameObject day = vp_Week.GetChild(vp_Week.childCount - 1).gameObject;
        for(int i=0;i< day.GetComponent<Day>().pn_Classess.childCount; i++) {
            Destroy(day.GetComponent<Day>().pn_Classess.GetChild(i).gameObject);
        }
        Debug.LogWarning("�ܼ�Ʈ �پ缺 �̱���");
        Button btn = Instantiate(PrefabContainer.instance.btn_Class, day.GetComponent<Day>().pn_Classess);
        btn.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite("rest");
        btn.GetComponent<ClassInfo>().info.name = "�ܼ�Ʈ";
        btn.GetComponent<ClassInfo>().info.prof = "���̵� �̸�";
        btn.GetComponent<ClassInfo>().info.des = "��ٸ��� ��ٸ��� �ܼ�Ʈ";
        btn.GetComponent<ClassInfo>().sprite = ImageLoader.instance.GetSprite("rest");
        btn.onClick.AddListener(() => ShowClassDetail(btn.GetComponent<ClassInfo>()));
        btn.onClick.AddListener(() => SetSelectedClass(btn.transform));
    }

    void SetSelectedClass(Transform btn) {
        selectedClass = btn.gameObject;

        if(selectedClass.transform.parent.parent == vp_Week.GetChild(0)) {
            if (!selectedClass.GetComponent<ClassInfo>().clear) {
                pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.interactable = true;
                pn_ClassInfo.GetComponent<pn_classinfo>().btn_tryrun.interactable = true;
            }
            else {
                pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.interactable = false;
                pn_ClassInfo.GetComponent<pn_classinfo>().btn_tryrun.interactable = false;
            }
        }
        else {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.interactable = false;
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_tryrun.interactable = false;
        }
    }

    void ShowClassDetail(ClassInfo info) {
        pn_ClassInfo.SetActive(true);
        pn_ClassInfo.GetComponent<pn_classinfo>().img_prof.sprite = info.sprite;
        pn_ClassInfo.GetComponent<pn_classinfo>().txt_prof.SetText(info.info.prof);
        pn_ClassInfo.GetComponent<pn_classinfo>().txt_des.SetText(info.info.des);
        pn_ClassInfo.GetComponent<pn_classinfo>().txt_name.SetText(info.info.name);
    }

    public void ClearClass() {
        print($"���� �Ϸ� : {selectedClass.GetComponent<ClassInfo>().info.name}");
        Instantiate(PrefabContainer.instance.img_Clear, selectedClass.transform);
        selectedClass.GetComponent<ClassInfo>().clear = true;
        Debug.LogWarning("���� ���� �̱���");
    }

    public void FailClass() {
        print($"���� ���� : {selectedClass.GetComponent<ClassInfo>().info.name}");
        Instantiate(PrefabContainer.instance.img_Fail, selectedClass.transform);
        selectedClass.GetComponent<ClassInfo>().clear = true;
        Debug.LogWarning("���� ���� �̱���");
    }

    void EmphasizeToday() {
        vp_Week.GetChild(0).GetComponent<Outline>().enabled = true;
    }

    public void ToTomorrow() {
        Debug.LogWarning("���Ϸ� ���� �ִϸ��̼� �̱���");
        DestroyImmediate(vp_Week.GetChild(0).gameObject);
        EmphasizeToday();
        CheckClear();
    }

    public void CheckClear() {
        GameObject clear = GameObject.FindGameObjectWithTag("clear");
        if(clear == null) {
            pn_WeekInfo.instance.btn_Totomorrow.interactable = false;
        }
        else {
            pn_WeekInfo.instance.btn_Totomorrow.interactable = true;
        }
    }

    public void SetClass() {
        pn_class.instance.img_prof.sprite = selectedClass.GetComponent<ClassInfo>().sprite;
        pn_class.instance.txt_classname.SetText(selectedClass.GetComponent<ClassInfo>().info.name);
        StartLesson();
    }

    public void ShowStudents() {
        Debug.LogWarning("���� �л� ���� �̱���");
    }

    void StartLesson() {
        Debug.LogWarning("���� �λ� �̱���");

        lst_lessons = new List<List<Lesson>>();
        phase = (int)Random.Range(0, 2 + level) + 1;
        for(int i = 0; i < phase; i++) {
            int lesson = (int)Random.Range(0, 2 + level) + 1;
            lst_lessons.Add(new List<Lesson>());
            for(int j = 0; j < lesson; j++) {
                Lesson l = LessonDataLoader.instance.GetLessonData(selectedClass.GetComponent<ClassInfo>().info.prof);
                lst_lessons[i].Add(l);
                //print($"phase {i} : {l.type} : {l.des}");
            }
        }

    }
}
