using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//SceneManagement
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("SettingRoom");  //대기실 화면으로 전환
    }
}
