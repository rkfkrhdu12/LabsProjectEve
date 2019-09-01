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
        if (skillMgr.skills[(int)pCtrl.skillCode].IsCool()) { End(); return; }

        skillMgr.skills[(int)pCtrl.skillCode].ReadySkill();
    }

    public override void UpdateState()
    {
        skillMgr.skills[(int)pCtrl.skillCode].UpdateSkill();
    }

    void End()
    {
        skillMgr.skills[(int)pCtrl.skillCode].End();

        pCtrl.ChangeState(ePlayerState.MOVE);
    }
}
