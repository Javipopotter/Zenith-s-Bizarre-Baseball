using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PathLevels", menuName = "ScriptableObjects/PathLevels", order = 1)]
public class PathLevels : ScriptableObject
{
    int playerIndex;
    int _veganLevel;

    public int veganLevel
    {
        get{return _veganLevel;}

        set
        {
            _veganLevel = value;

            switch (value)
            {
                case 2:
                    GameManager.GM.GoVegan(playerIndex);
                    break;
            }
        }
    }

    int _hunterLevel;

    int hunterLevel
    {
        get{return _hunterLevel;}
        set
        {
            _hunterLevel = value;

            switch (value)
            {
                case 2:
                    GameManager.GM.GoHunter(playerIndex);
                    break;
            }

            if(veganLevel > 0 && value > 0)
            {
                //Vegan Bretrayal
            }
        }
    }

    bool _isIndebted;

    public bool isIndebted
    {
        get{return _isIndebted;}
        set
        {
            _isIndebted = value;

            GameManager.GM.GetIndebted(playerIndex, value);
        }
    }

    bool _isGreedy;

}
