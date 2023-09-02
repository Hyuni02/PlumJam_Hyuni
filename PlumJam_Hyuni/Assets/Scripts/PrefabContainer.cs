using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabContainer : MonoBehaviour
{
    public GameObject Card_Detail;
    public GameObject Card_Simple;
    public GameObject WeekDay;
    public GameObject WeekEnd;
    public Button btn_Class;
    public GameObject img_Clear;

    public static PrefabContainer instance;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }
}
