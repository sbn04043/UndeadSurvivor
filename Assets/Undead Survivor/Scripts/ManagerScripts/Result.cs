using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject[] titles;

    public void Win()
    {
        titles[0].SetActive(true);
        AudioManager.instance.PlayBgm(false);
    }

    public void Lose()
    {
        titles[1].SetActive(true);
        AudioManager.instance.PlayBgm(false);
    }


}
