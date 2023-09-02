using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pn_WeekInfo : MonoBehaviour
{
    public static pn_WeekInfo instance;

    public Button btn_Totomorrow;

    private void Awake() {
        if (instance != null) {
            Destroy(this);
        }
        instance = this;
    }
}
