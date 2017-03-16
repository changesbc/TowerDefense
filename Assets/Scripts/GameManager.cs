using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public GameObject endUI;
    public Text endMessage;
    public static GameManager _instance;
    private EnemySpawner enemySpawner;

    void Awake()
    {
        _instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void Win()
    {
        endUI.SetActive(true);
        endMessage.text = "胜 利";
    }

    public void Failed()
    {
        endUI.SetActive(true);
        endMessage.text = "失 败";
        enemySpawner.Stop();
    }

    public void OnRetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuBtn()
    {
        SceneManager.LoadScene(0);
    }

}
