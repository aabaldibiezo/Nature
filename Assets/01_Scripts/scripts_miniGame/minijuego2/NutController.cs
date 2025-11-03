using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class NutController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float smoothTime = 0.2f;
    public float stopDistance = 0.1f;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Vector2 targetPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // actualizar posición objetivo del mouse
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;
        targetPos = mouseWorld;
    }

    void FixedUpdate()
    {
        if (BlockTimer.IsBlocked)
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true; // ❌ Congela físicas y colisiones
            return;
        }
        else
        {
            rb.isKinematic = false; // ✅ Rehabilita físicas cuando termina bloqueo
        }

        float distance = Vector2.Distance(rb.position, targetPos);

        if (distance > stopDistance)
        {
            Vector2 newPos = Vector2.SmoothDamp(rb.position, targetPos, ref currentVelocity, smoothTime, moveSpeed);
            rb.MovePosition(newPos);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }


}



