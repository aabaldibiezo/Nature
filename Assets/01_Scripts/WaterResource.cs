using UnityEngine;

public class WaterResource : MonoBehaviour
{
    public int cantidadMaxima = 500;
    public int cantidadActual;

    [Header("Distancia de recolección")]
    public float radioInteraccion = 1.5f; // distancia a la que el peón puede recolectar

    void Start()
    {
        cantidadActual = cantidadMaxima;
    }

    public int Extraer(int cantidad)
    {
        int extraido = Mathf.Min(cantidad, cantidadActual);
        cantidadActual -= extraido;
        return extraido;
    }

    public bool EstaAgotado()
    {
        return cantidadActual <= 0;
    }

    // Opcional: gizmo para ver el radio de interacción en la escena
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radioInteraccion);
    }
}
