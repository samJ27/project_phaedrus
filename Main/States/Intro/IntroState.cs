using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroState : MainState
{
	public CanvasGroup group;
	public IntroTypedText sceneIntroText;

    public override void Enter()
    {
        // set the active UI canvas
        group.gameObject.SetActive(true);
        group.alpha = 1;
        group.interactable = true;
        group.blocksRaycasts = true;
        sceneIntroText.gameObject.SetActive(false);
        group.gameObject.SetActive(true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public void PlaySceneIntro()
    {
        StartCoroutine(DoSceneIntro());
    }

    private IEnumerator DoSceneIntro()
    {
        Debug.Log("We are in the intro state");
        yield return new WaitForSeconds(2f);

        // Do some typed text to introduce the scene
        sceneIntroText.gameObject.SetActive(true);
        while (sceneIntroText.typedText.typing)
        {
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        Complete();
    }
}