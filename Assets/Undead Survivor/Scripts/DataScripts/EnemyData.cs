using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Object/EnemyData")]
public class EnemyData : ScriptableObject
{

    public enum EnemyType { Enemy0, Enemy1, Enemy2, Enemy3}

    [Header("Main Info")]
    public EnemyType enemyType;
    public int enemyId;
    //public string enemyName;

    public Sprite enemyIcon;

    [Header("Enemy Data")]
    public float maxHp;
    public int curHp;
    public float damage;
    public int speed;
}
