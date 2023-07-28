using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive1 : MonoBehaviour
{
    public GameObject CheckMark;

    void Start()
    {
        CheckMark.SetActive(false);
    }
}
