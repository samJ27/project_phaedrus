using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System;

public class SceneState : MainState
{
    //// The json compiled ink story
    //public TextAsset storyJSON;
    //// The ink runtime story object
    //private Story story;

    //// the text to write
    //public Transform contentParent;

    //// UI Prefabs
    //[SerializeField]
    //private Text textPrefab = null;
    //[SerializeField]
    //private Button buttonPrefab = null;

    public StoryControl storyControl;
    private bool isComplete;
    public override void Enter()
    {
        base.Enter();

        Debug.Log("We are in the scene state");

        // Instantiate new story
        //var storyControl = new StoryControl
        //{
        //    storyJson = storyJSON,
        //    textPrefab = textPrefab,
        //    buttonPrefab = buttonPrefab,
        //    contentParent = contentParent
        //};

        isComplete = storyControl.StartStory();
    }
}
