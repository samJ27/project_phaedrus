using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainState : MonoBehaviour
{
    // Declare the delegate
    public delegate void OnCompletionEvent();
    // Declare the event
    public event OnCompletionEvent OnCompletion;

    // Wrap events in virtual methods to enable
    // derived classes to raise the event
    public virtual void Enter()
    {

    }

    public virtual void Complete()
    {
        OnCompletion?.Invoke();
    }
    public virtual void Exit()
    {

    }

}
