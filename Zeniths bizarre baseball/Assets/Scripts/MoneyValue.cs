using UnityEngine;

public class MoneyValue : MonoBehaviour
{
    [SerializeField] int _value = 1;
    public int value{get{return _value;} set{_value = value;}}
    float _timeToVanish = 20;
    float _maxTimeToVanish = 20;
    SpriteRenderer sr;
    [SerializeField]float alpha = 1;
    int dir = 1;
    int blinksPerSecond = 16;

    private void Awake() {
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Player") || other.transform.CompareTag("bat"))
        {
            AudioManager.instance.PlayOneShot("collect_coin");
            GameManager.GM.money += value;
            gameObject.SetActive(false);
        }
    }

    private void OnEnable() {
        _timeToVanish = _maxTimeToVanish;
    }

    private void Update() {
        _timeToVanish -= Time.deltaTime;
        if(_timeToVanish <= 0)
        {
            gameObject.SetActive(false);
        }

        if(_timeToVanish <= 2f)
        {
            alpha -= Time.deltaTime * (blinksPerSecond / (_timeToVanish + 0.5f)) * dir;
            if(alpha < 0)
            {
                dir = -1;
            }
            else if(alpha > 1)
            {
                dir = 1;
            }
            sr.color = new Color(1, 1, 1, alpha);
        }
        else
        {
            sr.color = new Color(1, 1, 1, 1);
        }
    }
}