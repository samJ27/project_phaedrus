using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System;

public class StoryControl : MonoBehaviour
{

    public StoryControl() { }
    private Story story;

    public TextAsset storyJson;
    public Text textPrefab;
    public Button buttonPrefab;
    public Transform contentParent;

    public bool StartStory()
    {
        if (storyJson == null)
        {
            Debug.LogWarning("Drag a valid story JSON file into the StoryReader component.");
            enabled = false;
        }
        if (story == null)
        {
            story = new Story(storyJson.text);
        }

        RemoveChildren();

        while (story.canContinue)
        {
            string text = story.Continue();
            // remove whitespace from text
            text = text.Trim();
            // Display the text
            CreateContentView(text);
        };

        // Display any choices, if they exist
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                // Tell button to do stuff
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }
            return false;
        }
        else
        {
            // all choices are complete
            Button choice = CreateChoiceView("End of story");
            choice.onClick.AddListener(delegate
            {
                StoryComplete();
            });
            return true;
        }
    }

    private bool StoryComplete()
    {
        return true;
    }

    private Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(contentParent.transform, false);

        // Get text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        // Make button expand to fit text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Creates a textbox showing the the line of text
    void CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = text;
        storyText.transform.SetParent(contentParent.transform, false);
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        StartStory();
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        for (int i = contentParent.childCount - 1; i >= 0; i--)
        {
            Destroy(contentParent.GetChild(i).gameObject);
        }
    }
}
