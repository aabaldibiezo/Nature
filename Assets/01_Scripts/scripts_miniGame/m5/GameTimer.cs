using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                 // ← Necesario para usar "Text"
using UnityEngine.SceneManagement;   // ← Necesario para reiniciar la escena y salir

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 240f;    // Duración del temporizador
    public Text timerText;              // Referencia al objeto de texto UI
    public GameObject gameOverPanel;    // Panel que se muestra al perder

    private bool timerIsRunning = true; // Controla si el temporizador sigue activo

    void Start()
    {
        // Si hay un bloqueo activo, no iniciar el temporizador
        if (PlayerPrefs.HasKey("BlockEndTime"))
        {
            timerIsRunning = false;
            timeRemaining = 240f;
            UpdateTimerDisplay();
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
            return;
        }

        Time.timeScale = 1f;
        UpdateTimerDisplay();
        gameOverPanel.SetActive(false);
    }


    void Update()
    {
        if (BlockTimer.IsBlocked) return; // ❌ Pausa el temporizador mientras dure el bloqueo

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                UpdateTimerDisplay();
                GameOver();
            }
        }
    }



    // Actualiza el texto del temporizador en pantalla
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // Muestra la pantalla de Game Over
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pausa el juego

        // 👉 Activar bloqueo de 3 minutos
        BlockTimer blockTimer = FindObjectOfType<BlockTimer>();
        if (blockTimer != null)
            blockTimer.StartBlock();
    }


    // Reinicia la escena actual
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Cierra el juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego (esto solo funciona en la versión build).");
    }
}

