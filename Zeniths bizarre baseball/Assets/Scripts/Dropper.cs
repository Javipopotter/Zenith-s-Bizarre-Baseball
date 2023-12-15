using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] entity drop;
    [SerializeField] int quantity = 5;
    float dropChance = 0.5f;

    public void Drop()
    {
        for(int i = 0; i < quantity; i++)
        {
            if(Random.Range(0f, 1f) < dropChance)
            {
                GameObject g = ObjectPooler.pooler.GetObject(drop, transform.position);
                Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
                float random = Random.Range(0f, 260f);
                rb.velocity = new Vector2(Mathf.Cos(random), Mathf.Sin(random)) * Random.Range(5f, 40f);
            }
        }
    }
}
