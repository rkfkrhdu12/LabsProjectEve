using UnityEngine;

public class State
{
    protected PlayerController pCtrl;

    public virtual void Init()
    {
        pCtrl = GameManager.Instance.player.GetComponent<PlayerController>();
    }

    public virtual void ReadyState()
    {

    }

    public virtual void UpdateState()
    {

    }
}
