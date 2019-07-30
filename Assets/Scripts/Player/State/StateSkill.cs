using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSkill : State
{
    SkillManager skillMgr;

    public override void Init()
    {
        base.Init();
        
        skillMgr = pCtrl.GetComponent<SkillManager>();
    }

    public override void ReadyState()
    {
        skillMgr.skill[(int)pCtrl.skillCode].ReadySkill();
    }

    public override void UpdateState()
    {
        if (skillMgr.skill[(int)pCtrl.skillCode].IsCool()) { End(); return; }

        skillMgr.skill[(int)pCtrl.skillCode].UpdateSkill();
    }

    void End()
    {
        pCtrl.ChangeState(ePlayerState.MOVE);
    }
}
