using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonumentMouseOver : MonoBehaviour
{
    public GameObject text;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void OnMouseOver()
    {
        Debug.Log("Mouse is over monument");
        text.SetActive(true);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse is no longer on monument");
        text.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    private void OnMouseDown()
    {
        
    }
}
