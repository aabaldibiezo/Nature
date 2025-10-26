using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockRetryButton : MonoBehaviour
{
    public Button retryButton;

    void Start()
    {
        retryButton.interactable = !PlayerPrefs.HasKey("BlockEndTime");
    }

    void Update()
    {
        // Mientras exista bloqueo, desactiva el botón
        retryButton.interactable = !PlayerPrefs.HasKey("BlockEndTime");
    }
}

