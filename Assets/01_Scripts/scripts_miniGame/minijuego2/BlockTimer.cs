using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class BlockTimer : MonoBehaviour
{
    public GameObject blockPanel;    // Panel de bloqueo
    public Text countdownText;       // Texto UI simple (no TMP)

    // 🔹 Duración del bloqueo configurable desde Inspector
    public float blockDuration = 90f; // 1 minuto 30 segundos = 90 segundos
    private float remainingTime;

    private const string BLOCK_END_TIME_KEY = "BlockEndTime";

    // Variable estática que indica si el juego/jugador está bloqueado
    public static bool IsBlocked = false;

    void Start()
    {
        // Verifica si hay un bloqueo guardado en PlayerPrefs
        if (PlayerPrefs.HasKey(BLOCK_END_TIME_KEY))
        {
            string savedTime = PlayerPrefs.GetString(BLOCK_END_TIME_KEY, "0");
            double blockEndTime;

            if (double.TryParse(savedTime, NumberStyles.Any, CultureInfo.InvariantCulture, out blockEndTime))
            {
                double currentTime = GetUnixTime();

                if (currentTime < blockEndTime)
                {
                    remainingTime = (float)(blockEndTime - currentTime);
                    blockPanel.SetActive(true);
                    StopAllCoroutines();
                    StartCoroutine(Countdown());
                    return;
                }
            }

            PlayerPrefs.DeleteKey(BLOCK_END_TIME_KEY); // dato inválido o expirado
        }

        blockPanel.SetActive(false);
        IsBlocked = false;
    }

    public void StartBlock()
    {
        remainingTime = blockDuration; // usa el tiempo configurable
        double blockEndTime = GetUnixTime() + remainingTime;

        PlayerPrefs.SetString(BLOCK_END_TIME_KEY, blockEndTime.ToString(CultureInfo.InvariantCulture));
        PlayerPrefs.Save();

        blockPanel.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        IsBlocked = true; // bloquea al jugador/juego

        while (remainingTime > 0)
        {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            if (countdownText != null)
                countdownText.text = $"Debes esperar {minutes:00}:{seconds:00} minutos para volver a jugar.";

            yield return new WaitForSecondsRealtime(1f);
            remainingTime--;
        }

        if (countdownText != null)
            countdownText.text = "¡Ya puedes volver a jugar!";

        blockPanel.SetActive(false);
        PlayerPrefs.DeleteKey(BLOCK_END_TIME_KEY);
        IsBlocked = false; // desbloquea al jugador/juego
    }

    private double GetUnixTime()
    {
        return (System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds;
    }
}
