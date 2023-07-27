using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public GameObject MapSet;
 

    public void OnSelectMapButtonClicked()
    {
        MapSet.SetActive(true);
    }
}
