using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // opcional si luego quieres recargar la escena

public class Goal : MonoBehaviour
{
    private bool levelComplete = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelComplete) return;

        // verifica si el objeto que entró es la nuez
        if (collision.CompareTag("Player"))
        {
            levelComplete = true;
            Debug.Log("¡Ganaste!");

            // pausa el juego
            Time.timeScale = 0f;

            // muestra un mensaje simple
            ShowWinMessage();
        }
    }

    void ShowWinMessage()
    {
        // crea un mensaje en pantalla rápido (puedes mejorar esto con UI más adelante)
        GameObject textGO = new GameObject("WinText");
        var text = textGO.AddComponent<UnityEngine.UI.Text>();

        // requiere un Canvas en la escena
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<UnityEngine.UI.CanvasScaler>();
            canvasGO.AddComponent<UnityEngine.UI.GraphicRaycaster>();
        }

        text.transform.SetParent(canvas.transform);
        text.alignment = TextAnchor.MiddleCenter;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        text.fontSize = 40;
        text.color = Color.yellow;
        text.text = "¡Ganaste!";
        text.rectTransform.anchoredPosition = Vector2.zero;
        text.rectTransform.sizeDelta = new Vector2(400, 100);
    }
}

