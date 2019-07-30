using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public State[] state = new State[(int)ePlayerState.LAST];

    void Start()
    {
        state = new State[(int)ePlayerState.LAST];
        state[(int)ePlayerState.NONE] = new State();
        state[(int)ePlayerState.MOVE] = new StateMove();
        state[(int)ePlayerState.SKILL] = new StateSkill();
        state[(int)ePlayerState.UI] = new StateUI();

        for (int i = (int)ePlayerState.MOVE; i < (int)ePlayerState.LAST; ++i)
        {
            state[i].Init();
        }
    }
}
