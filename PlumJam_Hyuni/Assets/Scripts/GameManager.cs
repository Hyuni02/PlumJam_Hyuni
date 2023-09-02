using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public enum Week { 월, 화, 수, 목, 금, 주말};

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
        Debug.LogWarning("저장 데이터 불러오기 미구현");
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
        print($"다음 공연까지 {nextconcert}주");
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

        if (week == Week.주말) {
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
        btn.GetComponent<ClassInfo>().info.name = "주말은 쉬는 날";
        btn.GetComponent<ClassInfo>().info.prof = "주말";
        btn.GetComponent<ClassInfo>().info.des= "일주일 중 유일한 휴식시간";
        btn.GetComponent<ClassInfo>().sprite = ImageLoader.instance.GetSprite("rest");
        btn.onClick.AddListener(() => ShowClassDetail(btn.GetComponent<ClassInfo>()));
        btn.onClick.AddListener(() => SetSelectedClass(btn.transform));
    }

    void MakeConcert() {
        GameObject day = vp_Week.GetChild(vp_Week.childCount - 1).gameObject;
        for(int i=0;i< day.GetComponent<Day>().pn_Classess.childCount; i++) {
            Destroy(day.GetComponent<Day>().pn_Classess.GetChild(i).gameObject);
        }
        Debug.LogWarning("콘서트 다양성 미구현");
        Button btn = Instantiate(PrefabContainer.instance.btn_Class, day.GetComponent<Day>().pn_Classess);
        btn.GetComponent<Image>().sprite = ImageLoader.instance.GetSprite("concert");
        btn.GetComponent<ClassInfo>().info.name = "콘서트";
        btn.GetComponent<ClassInfo>().info.prof = "아이돌 이름";
        btn.GetComponent<ClassInfo>().info.des = "기다리고 기다리던 콘서트";
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

        if(info.info.name == "콘서트") {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("콘서트 관람");
        }
        else {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("강의 듣기");
        }
        if(info.info.prof == "주말") {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("휴식");
        }
        else {
            pn_ClassInfo.GetComponent<pn_classinfo>().btn_listen.GetComponentInChildren<TMP_Text>().SetText("강의 듣기");
        }
    }

    public void ClearClass() {
        print($"수강 완료 : {selectedClass.GetComponent<ClassInfo>().info.name}");
        Instantiate(PrefabContainer.instance.img_Clear, selectedClass.transform);
        selectedClass.GetComponent<ClassInfo>().clear = true;
        ChangeScore();
        GetNewStudent();
    }

    void GetNewStudent() {
        //랜덤으로 학생 하나를 뽑음
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

        //이미 있는 학생이면 해당 학생 체력 회복
        if(exist) {
            Target.current_HP = Target.HP;
            print($"{newstudent.image} 회복");
            Notification("체력 회복", $"{newstudent.Name}의 체력이 회복 되었습니다.", ImageLoader.instance.GetSprite(newstudent.image));
        }
        //새로운 학생이면 추가
        else {
            lst_Student.Add(newstudent);
            print($"새로운 {newstudent.image}");
            Notification("새로운 동료!", $"새로운 동아리원으로 {newstudent.Name}(이)가 들어왔습니다.", ImageLoader.instance.GetSprite(newstudent.image));
        }
    }

    void ChangeScore() {
        Debug.LogWarning("학점 변경 미구현");
    }

    public void FailClass() {
        print($"수강 실패 : {selectedClass.GetComponent<ClassInfo>().info.name}");
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
        Notification("둥근해가 떳습니다.", "자리에서 일어나서~ \n 수업들으로 가세요 당장", ImageLoader.instance.GetSprite("sunrise"));
        DestroyImmediate(vp_Week.GetChild(0).gameObject);

        if(vp_Week.childCount == 0) {
            Debug.LogWarning("다음주 넘어가기 미구현");
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
            Notification("나의 학점이...", "대학생활을 너무 대충하신거 아닌가요? \n 아무리 콘서트가 좋다고 하지만 학점은 당신의 인생을 따라다닌다구요?");
        }
    }

    public void SetClass() {
        if(selectedClass.GetComponent<ClassInfo>().info.prof == "주말") {
            foreach(var stu in lst_Student) {
                stu.current_HP = stu.HP;
            }
            ClearClass();
            pn_WeekInfo.instance.btn_Totomorrow.interactable = true;

            Debug.LogWarning("체력 회복 알림 미구현");

            Canvas_Class.SetActive(false);
            Canvas_Schedule.SetActive(true);
            return;
        }
        if(selectedClass.GetComponent<ClassInfo>().info.name == "콘서트") {
            foreach (var stu in lst_Student) {
                stu.current_HP = stu.HP;
            }
            ClearClass();
            pn_WeekInfo.instance.btn_Totomorrow.interactable = true;
            Canvas_Class.SetActive(false);
            Canvas_Schedule.SetActive(true);
            Notification("즐거운 콘서트", "좋아하던 아이돌을 실제로 눈앞에서 보다니 ㅠㅠ", ImageLoader.instance.GetSprite("concert"));
            pn_notification.transform.GetChild(0).GetChild(0).GetComponent<Button>().onClick.AddListener(() => {
                Notification("아쉽지만 여기까지", "현재 구현된 부분은 여기까지 입니다. \n 시간도 부족하고 실력도 부족해서 완성도도 떨어지지만 플레이 해주셔서 감사합니다.");
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
        pn_class.instance.txt_talk.SetText(GreetingDataLoader.instance.GetGreetingData(selectedClass.GetComponent<ClassInfo>().info.prof, "인사").talk);
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
        //판정 부분
        foreach(var assign in Assignment) {
            if(assign.image == "수업") {
                if (lst_Field.Count == 0) {
                    win = false;
                    Notification("수업을 안듣다니!", "아무도 수업을 듣지 않았군요! \n 체력 -1", null);
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
            if(assign.image == "질문") {
                win = false;
                for (int i = 0; i < lst_Field.Count; i++) {
                    if (lst_Field[i].Ability >= assign.Ability) {
                        lst_Field[i].current_HP--;
                        win = true;
                        Notification("질문에 대답을 못하다니!", "질문에 대답할 수 있는 사람이 한명도 없다니! 다들 수업을 대충 듣는 군요!! \n 체력 -1", null);
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
            if (assign.image == "연습문제") {
                win = false;
                for (int i = 0; i < lst_Field.Count; i++) {
                    lst_Field[i].current_HP--;
                    if (lst_Field[i].Ability >= assign.Ability) {
                        win = true;
                        Notification("다들 수업을 안들은 건가요?", "이 쉬운 문제를 아무도 풀지 못하다니 실망이군요! \n 체력 -1", null);
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
        //승리
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
            //패배
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
