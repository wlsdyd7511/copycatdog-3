using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModChage : MonoBehaviour
{
    // Start is called before the first frame update
    public void ModChange(int num)
    {
        if (GameMgr.Instance != null)
        {
            GameMgr.Instance.modNum = num;
        }
    }
}
