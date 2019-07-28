using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    static GameManager _intance;
    public static GameManager Instance
    {
        get
        {
            return _intance;
        }
        set
        {
            if(_intance == null)
            {
                _intance = new GameManager();
            }
        }
    }
    #endregion

    void Awake()
    {
        _intance = this;
    }

    public GameObject player;

    public string monsterTag = "Monster";
    public string playerTag = "Player";

}
