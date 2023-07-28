using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Check : MonoBehaviour
{
    public GameObject MapSet;

    public void OnCheckButtonClicked()
    {
        MapSet.SetActive(false);
    }


}
