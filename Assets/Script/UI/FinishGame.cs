using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishButtonClick : MonoBehaviour
{
    public void onFinishButtonClick()
    {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
