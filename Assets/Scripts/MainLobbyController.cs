using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLobbyController : MonoBehaviour
{
    public GameObject pn_SaveFiles;
    public GameObject pn_Setting;
    public GameObject pn_Quit;

    private void Start() {
        CloseAllPanel();
    }

    public void QuitGame() {
        Application.Quit();
    }

    void CloseAllPanel() {
        pn_SaveFiles.SetActive(false);
        pn_Setting.SetActive(false);
        pn_Quit.SetActive(false);
    }

    public void Open_SaveFiles() {
        CloseAllPanel();
        pn_SaveFiles.SetActive(true);
    }

    public void Open_Setting() {
        CloseAllPanel();
        pn_Setting.SetActive(true);
    }

    public void Open_Quit() {
        CloseAllPanel();
        pn_Quit.SetActive(true);
    }

    public void ToNextScene() {
        SceneManager.LoadScene("2.Field");
    }
}
