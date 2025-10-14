using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            int escenaActual = SceneManager.GetActiveScene().buildIndex;

           
            int siguienteEscena = escenaActual + 1;

            
            if (siguienteEscena < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(siguienteEscena);
            }
            else
            {
                Debug.Log("Has llegado al final del juego 🌱");
            }
        }
    }
}
