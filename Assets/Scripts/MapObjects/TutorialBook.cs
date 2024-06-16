using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBook : MonoBehaviour
{
    public GameObject tutorialPanel;
    bool nearPlayer;

    private void Update()
    {
        if(nearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            tutorialPanel.SetActive(!tutorialPanel.activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            nearPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            nearPlayer = false;
            if (tutorialPanel == null) return;
            tutorialPanel.SetActive(false);
        }
    }
}
