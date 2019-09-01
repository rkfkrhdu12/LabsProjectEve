using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionTrail : MonoBehaviour
{
    public GameObject trail;
    PlayerController pCtrl;

    private void Start()
    {
        pCtrl = GetComponent<PlayerController>();
        curState = eTrail.NONE;
    }

    enum eTrail
    {
        NONE,
        ENABLEOFF,
        OFFED,
        ENABLEON,
        ONED,
    }

    eTrail curState;

    void Update()
    {
        switch (curState)
        {
            case eTrail.NONE:
                if (pCtrl.s == ePlayerState.NONE)
                    curState = eTrail.ENABLEOFF;
                else
                    curState = eTrail.ENABLEON;
                break;
            case eTrail.ENABLEOFF:
                EnableTrail(false);
                curState = eTrail.OFFED;
                break;
            case eTrail.OFFED:
                if(pCtrl.s != ePlayerState.NONE)
                    curState = eTrail.ENABLEON;
                break;
            case eTrail.ENABLEON:
                EnableTrail(true);
                curState = eTrail.ONED;
                break;
            case eTrail.ONED:
                if (pCtrl.s == ePlayerState.NONE)
                    curState = eTrail.ENABLEOFF;
                break;
        }

        if (pCtrl.s == ePlayerState.NONE)
        {
            EnableTrail(false);
            
        }
        else
        {
            trail.GetComponent<MotionTrail>().enabled = (true);
        }
    }

    void EnableTrail(bool enabled)
    {
        for (int i = 0; i < trail.transform.childCount; ++i)
        {
            trail.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = enabled;
        }
    }
}
