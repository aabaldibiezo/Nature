using System.Collections;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedLives : MonoBehaviour
{
    public int maxLives = 10;
    private int currentLives;

    public Text livesText;               // Texto UI para mostrar vidas
    public GameTimer gameTimer;          // Referencia al script GameTimer para mostrar Game Over

    private bool isGameOver = false;

    void Start()
    {
        currentLives = maxLives;
        UpdateLivesDisplay();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isGameOver) return;

        if (collision.gameObject.CompareTag("Wall"))
        {
            LoseLife();
        }
    }

    void LoseLife()
    {
        currentLives--;
        UpdateLivesDisplay();

        if (currentLives <= 0)
        {
            isGameOver = true;

            if (gameTimer != null)
                gameTimer.GameOver();

            // 👉 Iniciar bloqueo de 3 minutos
            BlockTimer blockTimer = FindObjectOfType<BlockTimer>();
            if (blockTimer != null)
                blockTimer.StartBlock();
        }
    }

    void UpdateLivesDisplay()
    {
        if (livesText != null)
            livesText.text = "Vidas: " + currentLives;
        else
            Debug.LogWarning("livesText no asignado en SeedLives.");
    }
}



