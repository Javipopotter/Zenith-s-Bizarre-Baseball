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
    string[] object_names = {"pitcher", "man", "cart", "ball"};
    List<List<GameObject>> _typeOfObject = new List<List<GameObject>>();
    public static bool paused = false;
    [SerializeField] bool serialized_pause;
    public Transform backGrounds;
    [SerializeField] GameObject gameElementsContainer;
    public enum gameStates
    {
        playing, paused, cinematic, 
    }
    gameStates _stateOfGame;
    public gameStates stateOfGame
    {
        get{return _stateOfGame;}
        set{
            _stateOfGame = value;
            switch(value)
            {
                case gameStates.playing:
                break;
                case gameStates.paused:
                break;
                case gameStates.cinematic:
                break;
            }
        }
    }

    private void Awake() {
        paused = false;
        GM = this;
        uIManager = GetComponent<UIManager>();

        for(int i = 0; i < object_names.Length; i++)
        {
            _typeOfObject.Add(new List<GameObject>());
        }

        for(int i = 0; i < 100; i++)
        {
            for(int a = 0; a < object_names.Length; a++)
            {
                var obj = Instantiate(objects[a], gameElementsContainer.transform);
                _typeOfObject[a].Add(obj);
            }
        }

        for(int i = 0; i < object_names.Length; i++)
        {
            objectDict.Add(object_names[i], _typeOfObject[i]);
        }
    }

    IEnumerator SlowMotion()
    {
        float originalTime = Time.timeScale;
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = originalTime;
    }

    private void Start() {
        player = GameObject.Find("Player").GetComponent<Movement>();
    }

    public void CameraShake(int num)
    {
        StartCoroutine(uIManager.CameraShake(num));
    }

    public void OnPlayerLifeChange(int n)
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
                    DialoguesManager.dialoguesManager.skip = true;
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
        uIManager.SetSortingLayerInFront(false);
        uIManager.GameOver();
    }
    public void RestartGame()
    {
        print("Restart");
        uIManager.SetSortingLayerInFront(true);
        paused = false;
        player.Restart();
        uIManager.Restart();
        foreach(GameObject en in GameObject.FindGameObjectsWithTag("Enemy")){
            en.SetActive(false);
            Spawner.sp.enemyCount--;
        }
        foreach(GameObject en in GameObject.FindGameObjectsWithTag("ball")){
            en.SetActive(false);
        }
        Spawner.sp.PlayHorde();
    }

    public void SetScenario(int n)
    {
        for(int i = 0; i < backGrounds.childCount; i++)
        {
            if(i == n)
            {
                backGrounds.GetChild(n).gameObject.SetActive(true);
            }
            backGrounds.GetChild(n).gameObject.SetActive(false);
        }
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
        player.Restart();
        Spawner.sp.spawnLevel++;
        uIManager.PlayAn("Transition", 0, 0.7f);
        foreach (Collider2D col in GameObject.FindGameObjectWithTag("wall").GetComponents<Collider2D>())
        {
            col.isTrigger = false;
        }
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
