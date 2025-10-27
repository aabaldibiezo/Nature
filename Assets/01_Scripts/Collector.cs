using UnityEngine;
using System.Collections;

public class Collector : Unit
{
    public enum CollectorState { Idle, MovingToResource, Collecting, ReturningToBase }
    private CollectorState state = CollectorState.Idle;

    [Header("Recolección")]
    public int capacidadMaxima = 50;
    private int cantidadActual = 0;

    public WaterResource recursoActual;
    public Base basePrincipal;
    public float tiempoRecoleccion = 2f;
    private bool recolectando = false;

    protected override void Update()
    {
        base.Update();

        switch (state)
        {
            case CollectorState.Idle:
                break;

            case CollectorState.MovingToResource:
                if (recursoActual == null)
                {
                    CambiarEstado(CollectorState.Idle);
                    break;
                }

                float distRecurso = Vector3.Distance(transform.position, recursoActual.transform.position);

                if (distRecurso > recursoActual.radioInteraccion)
                {
                    if (!isMoving)
                        MoveTo(recursoActual.transform.position);
                }
                else
                {
                    isMoving = false;
                    CambiarEstado(CollectorState.Collecting);
                }
                break;

            case CollectorState.Collecting:
                if (!recolectando)
                    StartCoroutine(Recolectar());
                break;

            case CollectorState.ReturningToBase:
                if (basePrincipal == null)
                {
                    CambiarEstado(CollectorState.Idle);
                    break;
                }

                float distBase = Vector3.Distance(transform.position, basePrincipal.transform.position);

                if (distBase > 3f)
                {
                    if (!isMoving)
                        MoveTo(basePrincipal.transform.position);
                }
                else
                {
                    isMoving = false;
                    DepositarEnBase();
                }
                break;
        }
    }

    IEnumerator Recolectar()
    {
        recolectando = true;
        Debug.Log("Recolectando agua...");
        yield return new WaitForSeconds(tiempoRecoleccion);

        if (recursoActual == null)
        {
            CambiarEstado(CollectorState.Idle);
            recolectando = false;
            yield break;
        }

        int extraido = recursoActual.Extraer(10);
        cantidadActual = Mathf.Min(cantidadActual + extraido, capacidadMaxima);

        Debug.Log("Recolectado: " + cantidadActual + " / " + capacidadMaxima);

        recolectando = false;

        if (cantidadActual >= capacidadMaxima && basePrincipal != null)
            CambiarEstado(CollectorState.ReturningToBase);
        else if (recursoActual != null && !recursoActual.EstaAgotado())
            CambiarEstado(CollectorState.MovingToResource);
        else
            CambiarEstado(CollectorState.Idle);
    }

    private void DepositarEnBase()
    {
        if (basePrincipal != null && cantidadActual > 0)
        {
            basePrincipal.DepositarAgua(cantidadActual);
            Debug.Log("Depositó " + cantidadActual + " en la base.");
            cantidadActual = 0;
        }

        if (recursoActual != null)
            CambiarEstado(CollectorState.MovingToResource);
        else
            CambiarEstado(CollectorState.Idle);
    }

    private void CambiarEstado(CollectorState nuevoEstado)
    {
        state = nuevoEstado;
    }

    public void AsignarRecurso(WaterResource recurso)
    {
        recursoActual = recurso;
        if (recurso != null)
        {
            Debug.Log("Recurso asignado: " + recurso.name);
            CambiarEstado(CollectorState.MovingToResource);

            if (basePrincipal == null)
                basePrincipal = FindObjectOfType<Base>();
        }
    }
}
