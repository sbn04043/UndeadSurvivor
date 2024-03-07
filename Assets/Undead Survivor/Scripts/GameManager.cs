using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header("# Game Control")]
    public float gameTime;
    public float maxGameTime = 600f;
    public bool isLive;

    [Header("# Player Info")]
    public int playerId;
    public float health;
    public float maxHealth;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 10, 20, 40, 60, 90, 150, 210 };

    [Header("# Game Object")]
    public PoolManager pool;
    public Player player;
    public LevelUp uiLevelUp;
    public HandleManager uiHandleManager;
    public Result uiResult;
    public GameObject EnemyCleaner;

    private void Awake()
    {
        instance = this;
        maxHealth = 100;
        health = maxHealth;
        isLive = false;
    }

    private void Update()
    {
        if (!isLive)
            return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GameStart(int id)
    {
        playerId = id;
        health = maxHealth;
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerId % 2);
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        EnemyCleaner.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }


    public void GetExp(int _exp)
    {
        if (!isLive) return;
        exp += _exp;

        if (exp >= nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            exp -= nextExp[Mathf.Min(level, nextExp.Length - 1)];
            level++;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

    public void OpenOption()
    {
        Stop();
        uiHandleManager.ShowOption();
    }

    public void ApplyOption()
    {
        Resume();
        uiHandleManager.HideOption();
    }
}
