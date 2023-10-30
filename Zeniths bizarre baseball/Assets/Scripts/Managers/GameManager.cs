using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    UIManager uIManager;
    Dictionary<string, List<GameObject>> objectDict = new Dictionary<string, List<GameObject>>();
    [SerializeField]GameObject Menu;
    Movement player;
    [SerializeField] GameObject[] objects;
    string[] object_names = {"pitcher", "man", "cart", "ball", "coin"};
    List<List<GameObject>> _typeOfObject = new List<List<GameObject>>();
    public static bool paused = false;
    [SerializeField] bool serialized_pause;
    Spawner spawner;
    public Transform backGrounds;
    [SerializeField] GameObject gameElementsContainer;
    [SerializeField] Stats[] stats;
    StageSettings _currentStageSettings;
    public StageSettings currentStageSettings{get{return _currentStageSettings;} private set{_currentStageSettings = value; spawner.CurrentSettings = value;}}
    public Stage _currentStage;
    public Stage currentStage{get{return _currentStage;} private set{_currentStage = value;}}
    ScenesManager scenesManager;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] UpgradePackage upgrader;
    int _money = 0;
    public int money
    {
        get{return _money;}
        set{
            _money = value;
            uIManager.UpdateMoney();
        }
    }
    public enum gameStates
    {
        playing, paused, cinematic, death
    }
    gameStates _stateOfGame;
    public gameStates stateOfGame
    {
        get{return _stateOfGame;}
        set{
            switch(_stateOfGame)
            {
                case gameStates.playing:
                break;
                case gameStates.paused:
                break;
                case gameStates.cinematic:
                break;
                case gameStates.death:
                break;
            }

            _stateOfGame = value;

            switch(_stateOfGame)
            {
                case gameStates.playing:
                break;
                case gameStates.paused:
                break;
                case gameStates.cinematic:
                break;
                case gameStates.death:
                break;
            }
        }
    }

    private void Awake() {
        paused = false;
        GM = this;
        scenesManager = GetComponent<ScenesManager>();
        spawner = GetComponent<Spawner>();
        uIManager = GetComponent<UIManager>();
        player = GameObject.Find("Player").GetComponent<Movement>();

        for(int i = 0; i < object_names.Length; i++)
        {
            _typeOfObject.Add(new List<GameObject>());
        }

        for(int i = 0; i < 100; i++)
        {
            for(int a = 0; a < object_names.Length; a++)
            {
                var obj = Instantiate(objects[a], gameElementsContainer.transform);
                obj.SetActive(false);
                _typeOfObject[a].Add(obj);
            }
        }

        for(int i = 0; i < object_names.Length; i++)
        {
            objectDict.Add(object_names[i], _typeOfObject[i]);
        }

        ResetStats();
    }

    void ResetStats()
    {
        foreach(Stats stat in stats)
        {
            List<string> keys = new List<string>(stat.modifiers.Keys);
            for(int i = 0; i < stat.modifiers.Count; i++)
            {
                stat.modifiers[keys[i]] = 1;
            }
        }
    }

    IEnumerator SlowMotion()
    {
        float originalTime = Time.timeScale;
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = originalTime;
    }

    public void CameraShake(int num)
    {
        StartCoroutine(uIManager.CameraShake(num));
    }

    public void SetUpgrader(Vector2 upgraderPos)
    {
        upgrader.gameObject.SetActive(true);
        upgrader.transform.position = upgraderPos;
    }

    public void OnPlayerLifeChange(float n)
    {
        if(n < -1){return;}
        // uIManager.UpdateLifeBar(n1, n2);
        uIManager.UpdateLifes(n);
    }

    public GameObject GetObject(string key)
    {
        foreach(GameObject obj in objectDict[key])
        {
            if(obj.activeInHierarchy == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }

    public void GameElementsAreActive(bool n)
    {
        gameElementsContainer.SetActive(n);
    }

    private void Update() {
        serialized_pause = paused;
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused && Time.timeScale != 0){return;}
            if(!DialoguesManager.dialoguesManager.cinematic)
            {
                PauseSwitch();
            }
            else
            {
                if(uIManager._skipText.activeInHierarchy)
                {
                    uIManager.ShowSkipText(false);
                    DialoguesManager.dialoguesManager.skipDialog = true;
                }
                else
                {
                    uIManager.ShowSkipText(true);
                }
            }
        }

        // if(Input.GetMouseButtonDown(1))
        // {
        //     Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     BallExplosion(pos, 40, true, 5, 8);
        // }
    }

    public void PauseSwitch()
    {
        Menu.SetActive(!Menu.activeInHierarchy);
        if (Time.timeScale == 0)
        {
            SetTime(1);
        }
        else
        {
            SetTime(0);
        }
        paused = !paused;
    }

    public void SetTime(float time)
    {
        Time.timeScale = time;
    }

    public void GameOver()
    {
        // StartCoroutine(SlowMotion());
        print("GameOvers");
        paused = true;
        player.GetComponentInChildren<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID("OverUI");
        uIManager.GameOver();
        BossLifeBarIsActive(false);
    }
    public void RestartGame()
    {
        // print("Restart");
        // spawner.PlayHorde();
        // paused = false;
        // player.Restart();
        // uIManager.Restart();
        // DisableGameElements();
        Reload();
    }

    public void Reload()
    {
        scenesManager.Reload();
    }

    private void DisableGameElements()
    {
        foreach (GameObject en in GameObject.FindGameObjectsWithTag("coin"))
        {
            en.SetActive(false);
            Spawner.sp.enemyCount--;
        }

        foreach (GameObject en in GameObject.FindGameObjectsWithTag("ball"))
        {
            en.SetActive(false);
        }

        upgrader.gameObject.SetActive(false); 
    }

    public void OnStageCleared()
    {
        NextStageText();
        OpenGates();
        SetUpgrader(transform.position);
    }

    public void SetStage(Stage stage)
    {
        if(currentStage != null) currentStage.gameObject.SetActive(false);
        CinemachineConfiner2D confiner2D = vcam.GetComponent<CinemachineConfiner2D>();
        currentStage = stage;
        confiner2D.m_BoundingShape2D = currentStage.cameraLimit;
        currentStageSettings = currentStage.settings;
        currentStage.gameObject.SetActive(true);
        StartLevel();
    }

    public void OpenGates()
    {
        currentStage.OpenGates();
    }

    public void UpdateBossLifeBar(float value, float maxValue)
    {
        uIManager.UpdateBossLifeBar(value, maxValue);
    }

    public void BossLifeBarIsActive(bool enabled)
    {
        uIManager.BossLifeBarIsActive(enabled);
    }
    public void SetGameState(gameStates state)
    {
        stateOfGame = state;
    }

    public void Victory()
    {
        uIManager.PlayAn("Victory");
        Time.timeScale = 0;
    }

    public void EnemyCounterUpdate()
    {
        uIManager.PlayAn("CounterUpdate");
    }

    public void Transition()
    {
        uIManager.PlayAn("Transition");
    }

    public void NextStageText()
    {
        uIManager.PlayAn("NextStage");
    }

    public void StartLevel()
    {
        DisableGameElements();
        player.EnterZone();
        spawner.PlayHorde();
        uIManager.PlayAn("Transition", 0, 0.7f);
    }

    public void BallExplosion(Vector2 pos, float vel, bool hit, int min_balls, int max_balls, int ball_type)
    {
        int n_balls = min_balls;

        if(max_balls > 0){
            n_balls = Random.Range(min_balls, max_balls);
        }

        for(int i = 0; i < n_balls; i++)
        {
            float random = Random.Range(0f, 260f);
            Vector2 rand_dir = new Vector2(Mathf.Cos(random), Mathf.Sin(random));
            GameObject _ball = GetObject("ball");
            _ball.transform.position = pos;
            _ball.GetComponent<ball>().SetProperties(ball_type);
            _ball.GetComponent<Rigidbody2D>().velocity = rand_dir * vel;
        }
    }
}
