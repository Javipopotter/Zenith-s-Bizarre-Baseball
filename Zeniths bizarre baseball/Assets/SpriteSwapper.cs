using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] CharacterSprites characterSprites;
    [SerializeField] SpriteRenderer sr;

    public void Swap(CharacterSprites.StateSprite stateSprite)
    {
        print("Swapping to " + stateSprite);
        sr.sprite = characterSprites.sprites[(int)stateSprite];
    }
}
