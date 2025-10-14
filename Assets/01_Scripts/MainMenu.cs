using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
   
    public void PlayGame()
    {
        
        SceneManager.LoadScene("01_Bosque_fosil");
    }

    // Botón Salir
    public void QuitGame()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
