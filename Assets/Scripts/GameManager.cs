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
            if (_intance == null)
            {
                _intance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
            return _intance;
        }
        set
        {
            if(_intance == null)
            {
                _intance = GameObject.Find("GameManager").GetComponent<GameManager>();
            }
        }
    }
    #endregion

    void Awake()
    {
        if (_intance == null)
        {
            _intance = this;
        }
    }

    public GameObject player;

    public string monsterTag = "Monster";
    public string playerTag = "Player";

}
