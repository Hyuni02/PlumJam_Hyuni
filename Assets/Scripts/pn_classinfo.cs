using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pn_classinfo : MonoBehaviour
{
    public Image img_prof;
    public TMP_Text txt_name;
    public TMP_Text txt_prof;
    public TMP_Text txt_des;
    public Button btn_listen;
    public Button btn_tryrun;

    private void Start() {
        gameObject.SetActive(false);
    }
}
