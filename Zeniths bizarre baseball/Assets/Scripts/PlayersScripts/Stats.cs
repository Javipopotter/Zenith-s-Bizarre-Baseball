using UnityEngine;

[CreateAssetMenu(fileName = "stats", menuName = "stats")]
public class Stats : ScriptableObject
{
   public float speed;
   public float damage = 1;
   public float maxlifes;
   public float speedModifier = 1;
   public float dmgModifier = 1;
   public float knockbackModifier = 1;
   public float poise = 1;
   public float knockback;
}
