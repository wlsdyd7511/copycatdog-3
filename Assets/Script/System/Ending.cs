using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject background;
    public GameObject[] winer = new GameObject[2];
    // Start is called before the first frame update
    public void DisplayEnding(int num)
    {
        background.SetActive(true);
        winer[num].SetActive(true);
        StartCoroutine(NextScene(2f));
    }

    private System.Collections.IEnumerator NextScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("SettingRoom");
    }
}
