using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Day : MonoBehaviour
{
    public TMP_Text txt_day;
    public Transform pn_Classess;

    public void Set(Week week) {
        txt_day.SetText(week.ToString());
    }
}
