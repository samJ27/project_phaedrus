using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class handles all states of the game space e.g. intro, game, outro, pause, restart, quite
/// Which will need to be implemented for each section of the game i.e. each scene.
/// The scripts here control each scene's start / end / branching states. 
/// </summary>
public class Main : MonoSingleton<Main>
{
    [Header("Skips the intro (editor only)")]
    public bool skipIntro = false;
    public bool fastIntro = false;

    [HideInInspector]
    public MainState currentState;
    public IntroState introState;
    public SceneState sceneStartState;
    public BaseState baseState;
    public OutroState outroState;

    private bool _paused = false;

    public bool paused
    {
        get
        {
            return _paused;
        }
        set
        {
            if (_paused == value)
                return;
            _paused = value;
            Time.timeScale = _paused ? 0 : 1;
            if (paused)
            {
                //sceneState.settingsView.Show();
            }
        }
    }

    private void Awake()
    {
        introState.OnCompletion += OnCompleteIntro;
        sceneStartState.OnCompletion += OnCompleteSceneStart;
        baseState.OnCompletion += OnCompleteBase;
        outroState.OnCompletion += OnCompleteOutro;
    }
    void Start()
    {
        ChangeState(introState);
        introState.PlaySceneIntro();
    }

    private void OnCompleteIntro()
    {
        ChangeState(sceneStartState);
    }

    private void OnCompleteSceneStart()
    {
        ChangeState(baseState);
    }

    private void OnCompleteBase()
    {
        throw new NotImplementedException();
    }

    private void OnCompleteOutro()
    {
        throw new NotImplementedException();
    }

    void ChangeState(MainState state)
    //Handles the state transitions
    {
        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = state;
        currentState.Enter();
    }


 

    // Update is called once per frame
    void Update()
    {
        
    }
}
