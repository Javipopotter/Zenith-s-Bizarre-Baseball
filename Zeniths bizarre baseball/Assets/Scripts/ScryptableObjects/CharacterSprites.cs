using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSprites", menuName = "ScriptableObjects/CharacterSprites")]
public class CharacterSprites : ScriptableObject
{
    public enum StateSprite
    {
        Idle_0, Idle_1, Move_0, Move_1, MoveDown_0, MoveDown_1, MoveUp_0, MoveUp_1,
        Attack_0, Attack_1, AttackDown_0, AttackUp_0, Hurt, Sleep_0, Sleep_1, Sit,
        IdleHorizontal_0, IdleHorizontal_1, IdleUp_0, IdleUp_1,
    }
    public Sprite[] sprites;
}
