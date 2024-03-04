using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public SpriteRenderer spriter;

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.25f, -0.25f, 0);
    Vector3 rightPosReverse = new Vector3(-0.25f, -0.25f, 0);
    Quaternion rightRot = Quaternion.Euler(0, 0, 0);
    Quaternion rightRotReverse = Quaternion.Euler(0, 180, 0);

    Vector3 leftPos = new Vector3(-0.2f, -0.4f, 0);
    Vector3 leftPosReverse = new Vector3(0.2f, -0.4f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    private void Awake()
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate()
    {
        bool isReverse = player.flipX;

        //�ٰŸ� ����
        if (isLeft)
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        //���Ÿ� ����
        else
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }

    public void Init()
    {

    }
}
