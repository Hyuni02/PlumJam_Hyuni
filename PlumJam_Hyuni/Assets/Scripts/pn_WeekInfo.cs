using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pn_WeekInfo : MonoBehaviour
{
    public static pn_WeekInfo instance;

    public Button btn_Totomorrow;
    public TMP_Text txt_Dday;
    public GameObject pn_heart;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }
}
