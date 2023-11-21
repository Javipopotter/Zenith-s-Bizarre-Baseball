using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;
    UIManager uIManager;
    Dictionary<string, List<GameObject>> objectDict = new Dictionary<string, List<GameObject>>();
    [SerializeField]GameObject Menu;
    Movement[] players;
    [SerializeField] GameObject[] objects;
    [SerializeField] string[] object_names = {"pitcher", "man", "cart", "ball", "coin"};
    List<List<GameObject>> _typeOfObject = new List<List<GameObject>>();
    InputManager[] playerInputs;
    bool _paused = false;
    public bool paused
    {
        get{return _paused;}
        set{
            _paused = value;
            bool enabled = value ? false : true;
            SetAllPLayerInputs(enabled);
            Time.timeScale = value ? 0 : 1;
        }
    }
    Spawner spawner;
    public Transform backGrounds;
    public GameObject gameElementsContainer;
    [SerializeField] Stats[] stats;
    public Stage _currentStage;
    [SerializeField] StageSettings combatStageSettings;
    public Stage currentStage{get{return _currentStage;} private set{_currentStage = value;}}
    ScenesManager scenesManager;
    [SerializeField] CinemachineVirtualCamera vcam;
    [SerializeField] EventsMemory eventsMemory;
    StagesSpawner stagesSpawner;
    [SerializeField] int _money = 0;
    [SerializeField] NotificationSet notificationSet;
    public int money
    {
        get{return _money;}
        set{
            _money = value;
            uIManager.UpdateMoney();

            //Check if IndebtionMustBeAdded
            if(value < 0 != players[0].GetComponent<Movement>().indebted){
                GetIndebted(0, value < 0);
            }
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
    [HideInInspector] public UnityEvent OnGameOver;

    private void Awake() {
        GM = this;
        combatStageSettings.Reset();
        SearchForPlayers();
        stagesSpawner = GetComponent<StagesSpawner>();
        scenesManager = GetComponent<ScenesManager>();
        spawner = GetComponent<Spawner>();
        uIManager = GetComponent<UIManager>();

        combatStageSettings.OnStart();

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

    public void SearchForPlayers()
    {
        playerInputs = FindObjectsOfType<InputManager>();
        List<Movement> listOfPlayers = new List<Movement>();
        foreach(InputManager input in playerInputs)
        {
            listOfPlayers.Add(input.GetComponent<Movement>());
        }
        players = listOfPlayers.FindAll(x => x != null).ToArray();
    }

    void SetAllPlayersLayers(string layerName)
    {
        foreach(Movement player in players)
        {
            player.GetComponentInChildren<SpriteRenderer>().sortingLayerID = SortingLayer.NameToID(layerName);
        }
    }

    void RecoverAllPLayersLifes()
    {
        for(int i = 0; i < players.Length; i++)
        {
            RecoverLife(i);
        }
    }

    void AllPlayersEnterZone()
    {
        for(int i = 0; i < players.Length; i++)
        {
            Vector2 pos = new Vector2(currentStage.startPos.transform.position.x + i * 6, currentStage.startPos.transform.position.y);
            players[i].EnterZone();
            players[i].transform.position = pos;
        }
    }
    
    public void SetAllPLayerInputs(bool value)
    {
        foreach(InputManager input in playerInputs)
        {
            input.InputSetEnabled(value);
        }
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

    public void SloMo()
    {
        StartCoroutine(SlowMotion());
    }
    IEnumerator SlowMotion()
    {
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = 1;
    }

    public void CameraShake(int num)
    {
        StartCoroutine(uIManager.CameraShake(num));
    }

    public void SetUpgrader(bool value)
    {
        uIManager.SetUpgrader(value);
        SetAllPLayerInputs(!value);
        DisableGameElements();
    }

    public void SetProgressBar(bool value)
    {
        uIManager.SetProgressBar(value);
    }

    public void UpgradeStat(string modKey, float value)
    {
        stats[0].modifiers[modKey] += value;
        uIManager.SetUpgrader(false);
    }

    public void UpgradeAbility(string abilityName)
    {
        Invoke(abilityName, 0);
        uIManager.SetUpgrader(false);
    }

    #region AbilitiesUpgrade

        void MaxLifesUpOnce() => MaxLifesUp(1);
        void RecoverLife(int index) => players[index].GetComponent<LifesManager>().lifes++;
        void RecoverLifeTwice(int index) => players[index].GetComponent<LifesManager>().lifes += 2;

        public void GoVegan(int index){
            players[index].GetComponent<Movement>().veganism = true;
            notificationSet.Notify(0);
        }
        public void GoHunter(int index){
            players[index].GetComponent<Movement>().hunter = true;
            notificationSet.Notify(1);
            combatStageSettings.OnGoHunter();
        }
        void GetIndebted(int index, bool value){
            players[index].GetComponent<Movement>().indebted = value;
            notificationSet.Notify(3);
            combatStageSettings.IsIndebted(value);
        }

        public void AddVeganLevel()
        {
            players[0].GetComponent<PathLevelManager>().veganLevel++;
        }

        public void AddHunterLevel()
        {
            players[0].GetComponent<PathLevelManager>().hunterLevel++;
        }
    #endregion

    public void VeganBetrayal()
    {
        combatStageSettings.OnVeganBetrayal();
        notificationSet.Notify(2);
    }

    public void OnPlayerLifeChange(float n)
    {
        if(n < -1){return;}
        uIManager.UpdateLifes(n);
    }

    public void MaxLifesUp(int num)
    {
        stats[0].maxlifes += num;
        uIManager.MaxLifesUp((int)stats[0].maxlifes - 1);
        foreach(Movement player in players)
        {
            player.GetComponent<LifesManager>().lifes += num;
        }
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

    public void PauseGame()
    {
        if(paused && Time.timeScale != 0){return;}

        if(!uIManager.upgradePanel.activeInHierarchy)
        {
            if(!DialoguesManager.dialoguesManager.dialogueUI.activeInHierarchy)
            {
                PauseSwitch(Menu.activeInHierarchy ? false : true);
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
    }

    public void PauseSwitch(bool active)
    {
        paused = active;
        Menu.SetActive(active);
    }

    public void SetTime(float time)
    {
        Time.timeScale = time;
    }

    public void GameOver()
    {
        uIManager.GameOver();
        OnGameOver.Invoke();

        DisableGameElements();

        AudioManager.instance.Stop(currentStage.settings.musicalTheme);
        AudioManager.instance.Play("Death_theme");

        SetAllPlayersLayers("OverUI");
        SetAllPLayerInputs(false);
        
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
        }

        foreach (GameObject en in GameObject.FindGameObjectsWithTag("ball"))
        {
            en.SetActive(false);
        }
        // upgrader.gameObject.SetActive(false); 
    }

    public void OnStageCleared()
    {
        NextStageText();
        combatStageSettings.OnStageClear();
        OpenGates();
        SetUpgrader(true);
    }

    public void SetStage(Stage stage)
    {
        if(currentStage != null)
        { 
            currentStage.gameObject.SetActive(false);

            if(stage.settings.musicalTheme != currentStage.settings.musicalTheme)
            {
                AudioManager.instance.Stop(currentStage.settings.musicalTheme);
                AudioManager.instance.Play(stage.settings.musicalTheme);
            }
        }
        else
        {
            AudioManager.instance.Play(stage.settings.musicalTheme);
        }

        currentStage = stage;
        spawner.CurrentSettings = currentStage.settings;

        vcam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = currentStage.cameraLimit;
        vcam.m_Lens.OrthographicSize = currentStage.settings.cameraSize;

        uIManager.UpdateProgressBar(currentStage.stageNum, stagesSpawner.stagesNumber);

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
        AllPlayersEnterZone();
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

    private void Start() {
        if(!eventsMemory.InitialCutscene){DialoguesManager.dialoguesManager.ExecuteDialogViaKey("InitialCutscene");}
        eventsMemory.InitialCutscene = true;
    }

    public void OnMainMenu()
    {
        eventsMemory.InitialCutscene = false;
    }
}
