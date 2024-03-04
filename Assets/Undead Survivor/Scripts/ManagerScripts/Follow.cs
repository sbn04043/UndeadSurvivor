using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;
    Transform playerTr;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    void Update()
    {
        playerTr = GameManager.instance.player.transform;
        rect.position = Camera.main.WorldToScreenPoint(playerTr.position + Vector3.down);
    }
}
