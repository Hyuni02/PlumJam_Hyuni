using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pn_class : MonoBehaviour
{
    public static pn_class instance;

    public Image img_prof;
    public Button btn_assignment;
    public TMP_Text txt_classname;
    public TMP_Text txt_talk;

    public Image img_player;
    public Toggle btn_student;

    public GameObject pn_student;
    public GameObject pn_assignment;

    public GameObject pn_PlayerHand;
    public GameObject vp_PlayerHand;

    private void Awake() {
        if(instance != null) {
            Destroy(this);
        }
        instance = this;
    }
}