using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stats", menuName = "stats")]
public class Stats : ScriptableObject
{
   public float speed;
   public float damage = 1;
   public float maxlifes;
   public float knockback;
   public float poise = 1;

   public Dictionary<string, float> modifiers = new Dictionary<string, float>()
   {
      {"speed", 1},
      {"damage", 1},
      {"knockback", 1},
      {"poise", 1}
   };
}
