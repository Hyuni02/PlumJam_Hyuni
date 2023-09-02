using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public enum Week { ��, ȭ, ��, ��, ��, �ָ�};

public class GameManager : MonoBehaviour
{
    public Transform vp_Week;
    public GameObject pn_ClassInfo;
    public GameObject pn_Students;
    public GameObject vp_Students;

    public GameObject selectedClass;

    public GameObject Canvas_Schedule;
    public GameObject Canvas_Class;

    public int chance = 3;

    Week e_week;
    //int week = 1;
    //int maxweek = 14;
    int level = 1;
    int phase = 0;
    int cphase = 0;
    List<List<Lesson>> lst_lessons;

    public List<Card> Assignment = new List<Card>();
    public List<Card> lst_Student = new List<Card>();
    public List<Card> lst_Field = new List<Card>();

    void Start()
    {
        Debug.LogWarning("���� ������ �ҷ����� �̱���");
        Clear_Week();
        WeekSchedule();
        Canvas_Schedule.SetActive(true);
        Canvas_Class.SetActive(false);

        GetNewStudent();
        GetNewStudent();
        GetNewStudent();
        pn_notification.SetActive(false);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            //ToTomorrow();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("1.Lobby");
        }
    }

    public void GoLobby() {
        SceneManager.LoadScene("1.Lobby");
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
        btn.GetComponent<ClassInfo>().info.prof = "�ָ�";
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
        btn.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite("concert");
        btn.GetComponent<ClassInfo>().info.name = "�ܼ�Ʈ";
        btn.GetComponent<ClassInfo>().info.prof = "���̵� �̸�";
        btn.GetComponent<ClassInfo>().info.des = "��ٸ��� ��ٸ��� �ܼ�Ʈ";
        btn.GetComponent<ClassInfo>().sprite = ImageLoader.instance.GetSprite("concert");
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

        if(info.info.name == "�ܼ�Ʈ") {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("�ܼ�Ʈ ����");
        }
        else {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("���� ���");
        }
        if(info.info.prof == "�ָ�") {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("�޽�");
        }
        else {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("���� ���");
        }
    }

    public void ClearClass() {
        print($"���� �Ϸ� : {selectedClass.GetComponent<ClassInfo>().info.name}");
        Instantiate(PrefabContainer.instance.img_Clear, selectedClass.transform);
        selectedClass.GetComponent<ClassInfo>().clear = true;
        ChangeScore();
        GetNewStudent();
    }

    void GetNewStudent() {
        //�������� �л� �ϳ��� ����
        Card newstudent = new Card(StudentDataLoader.instance.GetStudentData().name);
        print($"new student : {newstudent.Name}");

        bool exist = false;
        Card Target = null;
        foreach(var stu in lst_Student) {
            if(stu.Name == newstudent.Name) {
                exist = true;
                Target = stu;
                break;
            }
        }

        //�̹� �ִ� �л��̸� �ش� �л� ü�� ȸ��
        if(exist) {
            Target.current_HP = Target.HP;
            print($"{newstudent.image} ȸ��");
            Notification("ü�� ȸ��", $"{newstudent.Name}�� ü���� ȸ�� �Ǿ����ϴ�.", ImageLoader.instance.GetSprite(newstudent.image));
        }
        //���ο� �л��̸� �߰�
        else {
            lst_Student.Add(newstudent);
            print($"���ο� {newstudent.image}");
            Notification("���ο� ����!", $"���ο� ���Ƹ������� {newstudent.Name}(��)�� ���Խ��ϴ�.", ImageLoader.instance.GetSprite(newstudent.image));
        }
    }

    void ChangeScore() {
        Debug.LogWarning("���� ���� �̱���");
    }

    public void FailClass() {
        print($"���� ���� : {selectedClass.GetComponent<ClassInfo>().info.name}");
        Instantiate(PrefabContainer.instance.img_Fail, selectedClass.transform);
        selectedClass.GetComponent<ClassInfo>().clear = true;
        chance--;
    }

    void EmphasizeToday() {
        vp_Week.GetChild(0).GetComponent<Outline>().enabled = true;
        pn_WeekInfo.instance.txt_Dday.SetText($"D-{vp_Week.childCount-1}");
        if(vp_Week.childCount == 1) {
            pn_WeekInfo.instance.txt_Dday.SetText($"D-Day");
        }
    }

    public void ToTomorrow() {
        Notification("�ձ��ذ� �����ϴ�.", "�ڸ����� �Ͼ��~ \n ���������� ������ ����", ImageLoader.instance.GetSprite("sunrise"));
        DestroyImmediate(vp_Week.GetChild(0).gameObject);

        if(vp_Week.childCount == 0) {
            Debug.LogWarning("������ �Ѿ�� �̱���");
            return;
        }

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

        if(chance == 3) {
            
        }
        if(chance == 2) {
            pn_WeekInfo.instance.pn_heart.transform.GetChild(0).gameObject.SetActive(false);
        }
        if(chance == 1) {
            pn_WeekInfo.instance.pn_heart.transform.GetChild(0).gameObject.SetActive(false);
            pn_WeekInfo.instance.pn_heart.transform.GetChild(1).gameObject.SetActive(false);
        }
        if (chance <= 0) {
            pn_WeekInfo.instance.pn_heart.transform.GetChild(0).gameObject.SetActive(false);
            pn_WeekInfo.instance.pn_heart.transform.GetChild(1).gameObject.SetActive(false);
            pn_WeekInfo.instance.pn_heart.transform.GetChild(2).gameObject.SetActive(false);
            Notification("���� ������...", "���л�Ȱ�� �ʹ� �����ϽŰ� �ƴѰ���? \n �ƹ��� �ܼ�Ʈ�� ���ٰ� ������ ������ ����� �λ��� ����ٴѴٱ���?");
        }
    }

    public void SetClass() {
        if(selectedClass.GetComponent<ClassInfo>().info.prof == "�ָ�") {
            foreach(var stu in lst_Student) {
                stu.current_HP = stu.HP;
            }
            ClearClass();
            pn_WeekInfo.instance.btn_Totomorrow.interactable = true;

            Debug.LogWarning("ü�� ȸ�� �˸� �̱���");

            Canvas_Class.SetActive(false);
            Canvas_Schedule.SetActive(true);
            return;
        }
        if(selectedClass.GetComponent<ClassInfo>().info.name == "�ܼ�Ʈ") {
            foreach (var stu in lst_Student) {
                stu.current_HP = stu.HP;
            }
            ClearClass();
            pn_WeekInfo.instance.btn_Totomorrow.interactable = true;
            Canvas_Class.SetActive(false);
            Canvas_Schedule.SetActive(true);
            Notification("��ſ� �ܼ�Ʈ", "�����ϴ� ���̵��� ������ ���տ��� ���ٴ� �Ф�", ImageLoader.instance.GetSprite("concert"));
            pn_notification.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                Notification("�ƽ����� �������", "���� ������ �κ��� ������� �Դϴ�. \n �ð��� �����ϰ� �Ƿµ� �����ؼ� �ϼ����� ���������� �÷��� ���ּż� �����մϴ�.");
            });
            return;
        }
        pn_class.instance.img_prof.sprite = selectedClass.GetComponent<ClassInfo>().sprite;
        pn_class.instance.txt_classname.SetText(selectedClass.GetComponent<ClassInfo>().info.name);
        StartLesson();
    }

    public void ToggleShowStudents(Toggle tog) {
        if (tog.isOn) {
            for (int i = 0; i < vp_Students.transform.childCount; i++) {
                Destroy(vp_Students.transform.GetChild(i).gameObject);
            }
            foreach (var card in lst_Student) {
                GameObject newCard = Instantiate(PrefabContainer.instance.Card_Detail, vp_Students.transform);
                newCard.GetComponent<Card_Detail>().Set(card);
            }
        }
        pn_Students.SetActive(tog.isOn);
    }

    void StartLesson() {
        cphase = 0;
        pn_class.instance.txt_talk.SetText(GreetingDataLoader.instance.GetGreetingData(selectedClass.GetComponent<ClassInfo>().info.prof, "�λ�").talk);
        pn_class.instance.pn_PlayerHand.SetActive(false);
        pn_class.instance.btn_student.GetComponent<Toggle>().isOn = false;
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
        print(phase);
        PlayProfessorTurn(0);
    }

    void PlayProfessorTurn(int phase) {
        for(int i = 0; i < pn_class.instance.pn_assignment.transform.childCount; i++) {
            Destroy(pn_class.instance.pn_assignment.transform.GetChild(i).gameObject);
        }
        Assignment.Clear();
        foreach (var card in lst_lessons[phase]) {
            Card c = new Card(card.prof, true);
            Assignment.Add(c);
        }
        foreach (var assign in Assignment) {
            GameObject ass = Instantiate(PrefabContainer.instance.Card_Simple, pn_class.instance.pn_assignment.transform);
            ass.GetComponent<Card_Simple>().Set(assign);
        }
        StartPlayerTurn();
    }

    void StartPlayerTurn() {
        pn_class.instance.pn_PlayerHand.SetActive(true);
        pn_class.instance.btn_student.GetComponent<Toggle>().isOn = true;
    }

    public void EndPlayerTurn() {
        bool win = true;
        //���� �κ�
        foreach(var assign in Assignment) {
            if(assign.image == "����") {
                if (lst_Field.Count == 0) {
                    win = false;
                    Notification("������ �ȵ�ٴ�!", "�ƹ��� ������ ���� �ʾұ���! \n ü�� -1", null);
                    break;
                }
                for (int i = 0; i < lst_Field.Count; i++) {
                    lst_Field[i].current_HP--;
                    if (lst_Field[i].current_HP <= 0) {
                        lst_Student.Add(lst_Field[i]);
                        lst_Field.Remove(lst_Field[i]);
                    }
                }
            }
            if(assign.image == "����") {
                win = false;
                for (int i = 0; i < lst_Field.Count; i++) {
                    if (lst_Field[i].Ability >= assign.Ability) {
                        lst_Field[i].current_HP--;
                        win = true;
                        Notification("������ ����� ���ϴٴ�!", "������ ����� �� �ִ� ����� �Ѹ� ���ٴ�! �ٵ� ������ ���� ��� ����!! \n ü�� -1", null);
                        break;
                    }
                }
                for (int i = 0; i < lst_Field.Count; i++) {
                    if (lst_Field[i].current_HP <= 0) {
                        lst_Student.Add(lst_Field[i]);
                        lst_Field.Remove(lst_Field[i]);
                    }
                }
            }
            if (assign.image == "��������") {
                win = false;
                for (int i = 0; i < lst_Field.Count; i++) {
                    lst_Field[i].current_HP--;
                    if (lst_Field[i].Ability >= assign.Ability) {
                        win = true;
                        Notification("�ٵ� ������ �ȵ��� �ǰ���?", "�� ���� ������ �ƹ��� Ǯ�� ���ϴٴ� �Ǹ��̱���! \n ü�� -1", null);
                        break;
                    }
                }
                for (int i = 0; i < lst_Field.Count; i++) {
                    if (lst_Field[i].current_HP <= 0) {
                        lst_Student.Add(lst_Field[i]);
                        lst_Field.Remove(lst_Field[i]);
                    }
                }
            }
        }
        UpdatePlayerHand();
        //�¸�
        if (win) {
            cphase++;
            if (cphase < phase) {
                PlayProfessorTurn(cphase);
                return;
            }
            else {
                Canvas_Class.SetActive(false);
                Canvas_Schedule.SetActive(true);
                ClearClass();
                CheckClear();
            }
        }
        else {
            //�й�
            Canvas_Class.SetActive(false);
            Canvas_Schedule.SetActive(true);
            FailClass();
            CheckClear();
        }
        ResetField();
    }

    void ResetField() {
         for(int i = 0; i < pn_class.instance.pn_student.transform.childCount; i++) {
            Destroy(pn_class.instance.pn_student.transform.GetChild(i).gameObject);
         }
        foreach (var card in lst_Field) {
            lst_Student.Add(card);
        }
        lst_Field.Clear();
    }

    public void TogglePlayerHand(Toggle tog) {
        pn_class.instance.pn_PlayerHand.SetActive(tog.isOn);
        UpdatePlayerHand();
    }

    void UpdatePlayerHand() {
        for(int i=0;i< pn_class.instance.vp_PlayerHand.transform.childCount; i++) {
            Destroy(pn_class.instance.vp_PlayerHand.transform.GetChild(i).gameObject);
        }

        foreach (var card in lst_Student) {
            GameObject newCard = Instantiate(PrefabContainer.instance.Card_Detail, pn_class.instance.vp_PlayerHand.transform);
            newCard.GetComponent<Card_Detail>().Set(card);
            newCard.GetComponent<Card_Detail>().btn_onclick.onClick.AddListener(() => OnClickCardDetail(newCard.GetComponent<Card_Detail>().c));
        }
        UpdateField();
    }
    
    void UpdateField() {
        for (int i = 0; i < pn_class.instance.pn_student.transform.childCount; i++) {
            Destroy(pn_class.instance.pn_student.transform.GetChild(i).gameObject);
        }

        foreach(var card in lst_Field) {
            GameObject newCard = Instantiate(PrefabContainer.instance.Card_Simple, pn_class.instance.pn_student.transform);
            newCard.GetComponent<Card_Simple>().Set(card);
            newCard.GetComponent<Card_Simple>().btn_onclick.onClick.AddListener(() => OnClickCardSimple(newCard.GetComponent<Card_Simple>().c));
        }
    }

    void OnClickCardDetail(Card card) {
        if (card.current_HP <= 0) return;
        lst_Field.Add(card);
        lst_Student.Remove(card);
        UpdatePlayerHand();
    }
    
    void OnClickCardSimple(Card card) {
        lst_Student.Add(card);
        lst_Field.Remove(card);
        UpdatePlayerHand();
    }

    public GameObject pn_notification;

    void Notification(string title, string content, Sprite sprite = null) {
        pn_notification.SetActive(true);
        if(sprite == null) {
            pn_notification.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
        }
        else {
            pn_notification.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        }
        pn_notification.transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>().SetText(title);
        pn_notification.transform.GetChild(0).GetChild(2).GetComponent<TMP_Text>().SetText(content);
        pn_notification.transform.GetChild(0).GetChild(3).GetComponent<Image>().sprite = sprite;
    }
}
