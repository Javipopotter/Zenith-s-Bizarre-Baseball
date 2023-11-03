using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] string drop;
    [SerializeField] int cuantity = 5;
    float dropChance = 0.5f;

    public void Drop()
    {
        if(Random.Range(0f, 1f) < dropChance)
        {
            for(int i = 0; i < cuantity; i++)
            {
                if(Random.Range(0f, 1f) < dropChance)
                {
                    GameObject g = GameManager.GM.GetObject(drop);
                    g.transform.position = transform.position;
                    Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                    float random = Random.Range(0f, 260f);
                    rb.velocity = new Vector2(Mathf.Cos(random), Mathf.Sin(random)) * Random.Range(5f, 40f);
                }
            }
        }
    }
}
