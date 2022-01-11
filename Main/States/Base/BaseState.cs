using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls base exploration of a scene
/// </summary>
public class BaseState : MainState
{
    public CanvasGroup group;

    // Start is called before the first frame update
    public override void Enter()
    {
        group.gameObject.SetActive(false);
        base.Enter();
        Debug.Log("We are in the base scene state");
    }
    public override void Exit()
    {
        base.Exit();
    }
}
