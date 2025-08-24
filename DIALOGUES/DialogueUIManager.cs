using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager Instance { get; private set; }

    public Canvas mainCanvas;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        HideCanvas();
    }

    public void MoveCanvasToNPC(DialogueAnchor dialogueAnchor)
    {
        mainCanvas.transform.position = dialogueAnchor.transform.position;
        mainCanvas.transform.rotation = dialogueAnchor.transform.rotation;

        DisplayCanvas();
    }

    public void DisplayCanvas()
    {
        if (mainCanvas != null && mainCanvas.enabled == false) mainCanvas.enabled = true;
    }

    public void HideCanvas()
    {
        if (mainCanvas != null && mainCanvas.enabled == true) mainCanvas.enabled = false;
    }
}
