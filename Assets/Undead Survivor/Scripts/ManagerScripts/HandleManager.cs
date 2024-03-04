using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HandleManager : MonoBehaviour
{ 
    public Slider[] sliders;
    public float[] fillArea;
    public RectTransform rect;
    public HandleManager instance;

    public enum Option { Light, Bgm, Sfx };
    public Option option;

    private void Awake()
    {
        instance = this;
        sliders = GetComponentsInChildren<Slider>(true);
        fillArea = new float[sliders.Length];
        rect = GetComponent<RectTransform>();
        //rect.localScale = Vector3.zero;
        instance.enabled = false;
    }

    private void Update()
    {
        for(int index = 0; index < sliders.Length; index++)
        {
            fillArea[index] = sliders[index].value;
        }
    }

    public void ShowOption()
    {
        rect.localScale = Vector3.one;
        instance.enabled = true;
        sliders[1].value = PlayerPrefs.GetFloat("Bgm");
        sliders[2].value = PlayerPrefs.GetFloat("Sfx");
    }

    public void HideOption()
    {
        rect.localScale = Vector3.zero;
        PlayerPrefs.SetFloat("Bgm", fillArea[1]);
        PlayerPrefs.SetFloat("Sfx", fillArea[2]);
        instance.enabled = false;
    }
}
