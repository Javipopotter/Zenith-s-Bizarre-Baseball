using System.Collections.Generic;
using UnityEngine;

public class PathLevelManager : MonoBehaviour
{
    [SerializeField] int playerIndex;
    [SerializeField] int _veganLevel;

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

    [SerializeField] int _hunterLevel;

    public int hunterLevel
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

            if(veganLevel == 2 && value > 0)
            {
                GameManager.GM.VeganBetrayal();
            }
        }
    }

    [SerializeField] bool _isIndebted;

    public bool isIndebted
    {
        get{return _isIndebted;}
        set{_isIndebted = value;}
    }

    bool _isGreedy;
}

