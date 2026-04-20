using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using Assets.Scripts;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public List<GameObject> Enemies;

    public GameObject EnemyPrefab;
    public GameObject PathPrefab;
    public GameObject TowerPrefab;

    public Transform[] Waypoints;

    private GameObject PathPiecesParent;
    private GameObject WaypointsParent;

    private LevelStuffFromXML levelStuffFromXML;

    public CarrotSpawner CarrotSpawner;

    [HideInInspector]
    public int MoneyAvailable { get; private set; }

    [HideInInspector]
    public float MinCarrotSpawnTime;

    [HideInInspector]
    public float MaxCarrotSpawnTime;

    public int Lives = 10;

    private int currentRoundIndex = 0;

    [HideInInspector]
    public GameState CurrentGameState;

    public SpriteRenderer BunnyGeneratorSprite;

    [HideInInspector]
    public bool FinalRoundFinished;

    private object lockerObject = new object();

    void Start()
    {
        IgnoreLayerCollisions();

        Enemies = new List<GameObject>();
        PathPiecesParent = GameObject.Find("PathPieces");
        WaypointsParent = GameObject.Find("Waypoints");

        levelStuffFromXML = Utilities.ReadXMLFile();

        CreateLevelFromXML();

        CurrentGameState = GameState.Start;
        FinalRoundFinished = false;
    }

    private void CreateLevelFromXML()
    {
        foreach (var position in levelStuffFromXML.Paths)
        {
            GameObject go = Instantiate(PathPrefab, position, Quaternion.identity);
            go.GetComponent<SpriteRenderer>().sortingLayerName = "Path";
            go.transform.parent = PathPiecesParent.transform;
        }

        for (int i = 0; i < levelStuffFromXML.Waypoints.Count; i++)
        {
            GameObject go = new GameObject();
            go.transform.position = levelStuffFromXML.Waypoints[i];
            go.transform.parent = WaypointsParent.transform;
            go.tag = "Waypoint";
            go.name = "Waypoints" + i.ToString();
        }

        GameObject tower = Instantiate(TowerPrefab, levelStuffFromXML.Tower, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";

        Waypoints = GameObject.FindGameObjectsWithTag("Waypoint")
            .OrderBy(x => x.name)
            .Select(x => x.transform)
            .ToArray();

        MoneyAvailable = levelStuffFromXML.InitialMoney;
        MinCarrotSpawnTime = levelStuffFromXML.MinCarrotSpawnTime;
        MaxCarrotSpawnTime = levelStuffFromXML.MaxCarrotSpawnTime;
    }

    private void IgnoreLayerCollisions()
    {
        int bunnyLayerID = LayerMask.NameToLayer("Bunny");
        int enemyLayerID = LayerMask.NameToLayer("Enemy");
        int arrowLayerID = LayerMask.NameToLayer("Arrow");
        int bunnyGeneratorLayerID = LayerMask.NameToLayer("BunnyGenerator");
        int backgroundLayerID = LayerMask.NameToLayer("Background");
        int pathLayerID = LayerMask.NameToLayer("Path");
        int towerLayerID = LayerMask.NameToLayer("Tower");
        int carrotLayerID = LayerMask.NameToLayer("Carrot");

        Physics2D.IgnoreLayerCollision(bunnyLayerID, enemyLayerID);
        Physics2D.IgnoreLayerCollision(arrowLayerID, bunnyGeneratorLayerID);
        Physics2D.IgnoreLayerCollision(arrowLayerID, backgroundLayerID);
        Physics2D.IgnoreLayerCollision(arrowLayerID, pathLayerID);
        Physics2D.IgnoreLayerCollision(arrowLayerID, bunnyLayerID);
        Physics2D.IgnoreLayerCollision(arrowLayerID, towerLayerID);
        Physics2D.IgnoreLayerCollision(arrowLayerID, carrotLayerID);
    }

    IEnumerator NextRound()
    {
        yield return new WaitForSeconds(2f);

        Round currentRound = levelStuffFromXML.Rounds[currentRoundIndex];

        for (int i = 0; i < currentRound.NoOfEnemies; i++)
        {
            GameObject enemy = Instantiate(EnemyPrefab, Waypoints[0].position, Quaternion.identity);

            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.Speed += Mathf.Clamp(currentRoundIndex, 1f, 5f);
            enemyComponent.EnemyKilled += OnEnemyKilled;

            Enemies.Add(enemy);

            yield return new WaitForSeconds(1f / (currentRoundIndex == 0 ? 1 : currentRoundIndex));
        }
    }

    void OnEnemyKilled(object sender, EventArgs e)
    {
        bool startNewRound = false;

        lock (lockerObject)
        {
            if (Enemies.Where(x => x != null).Count() == 0 && CurrentGameState == GameState.Playing)
            {
                startNewRound = true;
            }
        }

        if (startNewRound)
            CheckAndStartNewRound();
    }

    private void CheckAndStartNewRound()
    {
        if (currentRoundIndex < levelStuffFromXML.Rounds.Count - 1)
        {
            currentRoundIndex++;
            StartCoroutine(NextRound());
        }
        else
        {
            FinalRoundFinished = true;
        }
    }

    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                if (Input.GetMouseButtonUp(0))
                {
                    CurrentGameState = GameState.Playing;
                    StartCoroutine(NextRound());
                    CarrotSpawner.StartCarrotSpawning();
                }
                break;

            case GameState.Playing:
                if (Lives == 0)
                {
                    StopAllCoroutines();
                    DestroyExistingEnemiesAndCarrots();
                    CarrotSpawner.StopCarrotSpawning();
                    CurrentGameState = GameState.Lost;
                }
                else if (FinalRoundFinished && Enemies.Where(x => x != null).Count() == 0)
                {
                    DestroyExistingEnemiesAndCarrots();
                    CarrotSpawner.StopCarrotSpawning();
                    CurrentGameState = GameState.Won;
                }
                break;

            case GameState.Won:
            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                break;
        }

        DebugUI(); // 👈 thay UI bằng log
    }

    void DebugUI()
    {
        switch (CurrentGameState)
        {
            case GameState.Playing:
                Debug.Log("Money: " + MoneyAvailable +
                          " | Life: " + Lives +
                          " | Round: " + (currentRoundIndex + 1));
                break;
        }
    }

    private void DestroyExistingEnemiesAndCarrots()
    {
        foreach (var item in Enemies)
        {
            if (item != null)
                Destroy(item);
        }

        var carrots = GameObject.FindGameObjectsWithTag("Carrot");

        foreach (var item in carrots)
        {
            Destroy(item);
        }
    }

    public void AlterMoneyAvailable(int money)
    {
        MoneyAvailable += money;

        Color temp = BunnyGeneratorSprite.color;
        temp.a = (MoneyAvailable < Constants.BunnyCost) ? 0.3f : 1.0f;
        BunnyGeneratorSprite.color = temp;
    }
}