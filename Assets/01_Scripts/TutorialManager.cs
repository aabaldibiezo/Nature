using UnityEngine;
public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialImage;   // Imagen o panel del tutorial
    public GameObject panelRecursos;   // Panel de recursos (HUD)
    private bool tutorialActive = true;
    void Start()
    {
        // Mostrar el tutorial al inicio
        if (tutorialImage != null)
            tutorialImage.SetActive(true);
        // Ocultar el panel de recursos durante el tutorial
        if (panelRecursos != null)
            panelRecursos.SetActive(false);
        // Pausar el juego
        Time.timeScale = 0f;
    }
    void Update()
    {
        // Si el tutorial está activo y se presiona cualquier tecla...
        if (tutorialActive && Input.anyKeyDown)
        {
            // Ocultar tutorial
            if (tutorialImage != null)
                tutorialImage.SetActive(false);
            // Mostrar el panel de recursos
            if (panelRecursos != null)
                panelRecursos.SetActive(true);
            // Reanudar el juego
            Time.timeScale = 1f;
            tutorialActive = false;
        }
    }
}