using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class InkTesting : MonoBehaviour
{
    public TextAsset inkJson;
    private Story story;

    public TextMeshProUGUI textPrefab;
    public Button buttonPrefab;

    void Start()
    {
        story = new Story(inkJson.text);

        refreshUI();
    }

    private void refreshUI()
    {
        eraseUI();

        TextMeshProUGUI storyText = Instantiate(textPrefab);

        string text = loadStoryChunk();

        List<string> tags = story.currentTags;

        if (tags.Count > 0)
        {
            text = tags[0] + " - " + text;
        }

        storyText.text = text;
        storyText.transform.SetParent(this.transform, false);

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab);
            choiceButton.transform.SetParent(this.transform, false); // set that button as a child of the canvas

            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>(); // Get the component as a TMP text object
            choiceText.text = choice.text; // set that component to the story text

            choiceButton.onClick.AddListener(delegate
                {
                    chooseStoryChoice(choice);
                }
            );
        }
    }

    void eraseUI()
    {
        for(int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
    }
}
