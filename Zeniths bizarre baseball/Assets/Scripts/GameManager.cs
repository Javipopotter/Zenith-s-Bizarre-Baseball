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

    private void Awake() {
        GM = this;
        uIManager = GetComponent<UIManager>();
        #region 
        #endregion

        for(int i = 0; i < object_names.Length; i++)
        {
            _typeOfObject.Add(new List<GameObject>());
        }

        for(int i = 0; i < 100; i++)
        {
            for(int a = 0; a < object_names.Length; a++)
            {
                var obj = Instantiate(objects[a]);
                _typeOfObject[a].Add(obj);
            }
        }

        for(int i = 0; i < object_names.Length; i++)
        {
            objectDict.Add(object_names[i], _typeOfObject[i]);
        }
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

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!DialoguesManager.dialoguesManager.cinematic)
            {
                Menu.SetActive(!Menu.activeInHierarchy);
                if(Time.timeScale == 0){
                    SetTime(1);
                }else{
                    SetTime(0);
                }
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

    public void SetTime(float time)
    {
        Time.timeScale = time;
    }

    public void RestartGame()
    {
        uIManager.SetSortingLayerInFront(true);
        DialoguesManager.dialoguesManager.cinematic = false;
        player.Restart();
        uIManager.PlayAn("Restart");
        foreach(GameObject en in GameObject.FindGameObjectsWithTag("Enemy")){
            en.SetActive(false);
            Spawner.sp.enemyCount--;
        }
        foreach(GameObject en in GameObject.FindGameObjectsWithTag("ball")){
            en.SetActive(false);
        }
        Spawner.sp.PlayHorde();
    }

    public void GameOver()
    {
        DialoguesManager.dialoguesManager.cinematic = true;
        uIManager.SetSortingLayerInFront(false);
        uIManager.PlayAn("GameOver");
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
