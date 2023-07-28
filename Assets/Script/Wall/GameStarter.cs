using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameMgr.Instance != null)
        {
            GameMgr.Instance.GameStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
