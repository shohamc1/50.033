using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject spawnManagerObject;
    private SpawnManager spawnManager;
    public Text score;
    // private int playerScore = 0;
    [SerializeField]
    private FloatSO scoreSO;

    // Game Over
    GameObject[] gameoverObjects;

    // Singleton Pattern
    private static GameManager _instance;
    // Getter
    public static GameManager Instance
    {
        get { return _instance; }
    }

    void Start()
    {
        spawnManager = spawnManagerObject.GetComponent<SpawnManager>();

        gameoverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameover");
        foreach (GameObject g in gameoverObjects)
        {
            g.SetActive(false);
        }

        // subscribe to player event
        GameManager.OnPlayerDeath += GameoverSequence;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameoverObjects = GameObject.FindGameObjectsWithTag("ShowOnGameover");
        foreach (GameObject g in gameoverObjects)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void increaseScore()
    {
        // playerScore += 1;
        // score.text = "SCORE: " + playerScore.ToString();
        scoreSO.Value += 1;
        score.text = "SORE: " + scoreSO.Value;
        spawnManager.spawnFromPooler(ObjectType.gombaEnemy);
    }

    public void damagePlayer()
    {
        OnPlayerDeath();
    }

    public delegate void gameEvent();

    public static event gameEvent OnPlayerDeath;

    void GameoverSequence()
    {
        // Mario dies
        Debug.Log("Game Over");
        // do whatever you want here, animate etc
        // ...
        showGameover();
    }
    void showGameover()
    {
        Time.timeScale = 0.0f;
        foreach (GameObject g in gameoverObjects)
        {
            g.SetActive(true);
        }
    }


}
