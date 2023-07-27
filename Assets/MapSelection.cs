using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelection : MonoBehaviour
{
    public GameObject MapSet;
    public GameObject SelectButton;

    public void OnSelectMapButtonClicked()
    {
        MapSet.SetActive(true);
    }
}
