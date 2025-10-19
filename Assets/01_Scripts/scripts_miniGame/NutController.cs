using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NutController : MonoBehaviour
{
    public float moveSpeed = 5f;       // velocidad base (ajusta desde el Inspector)
    public float smoothTime = 0.2f;    // tiempo de suavizado (mayor = más lento)
    public float stopDistance = 0.1f;  // distancia mínima al cursor antes de detenerse

    private Rigidbody2D rb;
    private Vector2 currentVelocity;   // usado por SmoothDamp
    private Vector2 targetPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // convertir posición del mouse a mundo
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        targetPos = mouseWorld;
    }

    void FixedUpdate()
    {
        // calcula distancia actual
        float distance = Vector2.Distance(rb.position, targetPos);

        if (distance > stopDistance)
        {
            // movimiento más suave y menos brusco que MoveTowards
            Vector2 newPos = Vector2.SmoothDamp(rb.position, targetPos, ref currentVelocity, smoothTime, moveSpeed);
            rb.MovePosition(newPos);
        }
        else
        {
            // detiene el movimiento si ya está cerca del cursor
            rb.velocity = Vector2.zero;
        }
    }
}


