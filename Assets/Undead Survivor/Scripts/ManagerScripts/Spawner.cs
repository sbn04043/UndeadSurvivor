using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;
    enum Level { Zero, One, Two, Three }
    int level;
    float timer;
    float bossTimer;

    private void Awake()
    {
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / (spawnData.Length + 1);
        bossTimer = levelTime / 2;
    }
    void Update()
    {
        if (!GameManager.instance.isLive)
            return;

        timer += Time.deltaTime;
        bossTimer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length);

        if (timer > spawnData[level].spawnTime)
        {
            Spawn(level);
            if (level > 0)
                Spawn(level - 1);
            timer = 0f;
        }
        
        if (bossTimer > levelTime)
        {
            BossSpawn(level);
            bossTimer = 0f;
        }
    }

    void Spawn(int _level)
    {
        GameObject enemy = GameManager.instance.pool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[_level]);
    }

    void BossSpawn(int _level)
    {
        GameObject boss = GameManager.instance.pool.Get(3);
        boss.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        boss.GetComponent<Enemy>().BossInit(spawnData[_level]);
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int health;
    public float speed;
    public int exp;
}