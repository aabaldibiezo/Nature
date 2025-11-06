using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Velocidad de movimiento")]
    public float moveSpeed = 20f;

    [Header("Bordes de la pantalla (en píxeles)")]
    public float borderThickness = 10f;

    [Header("Límites del mapa")]
    public Vector2 xLimits = new Vector2(-50, 50);
    public Vector2 zLimits = new Vector2(-50, 50);

    [Header("Zoom de cámara")]
    public float zoomSpeed = 100f;
    public float minY = 10f;
    public float maxY = 60f;

    private void Update()
    {
        Vector3 pos = transform.position;

        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            pos.z += moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            pos.z -= moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            pos.x += moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            pos.x -= moveSpeed * Time.deltaTime;

        
        if (Input.mousePosition.y >= Screen.height - borderThickness)
            pos.z += moveSpeed * Time.deltaTime;
        if (Input.mousePosition.y <= borderThickness)
            pos.z -= moveSpeed * Time.deltaTime;
        if (Input.mousePosition.x >= Screen.width - borderThickness)
            pos.x += moveSpeed * Time.deltaTime;
        if (Input.mousePosition.x <= borderThickness)
            pos.x -= moveSpeed * Time.deltaTime;

        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * zoomSpeed * Time.deltaTime * 100f; 
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

       
        pos.x = Mathf.Clamp(pos.x, xLimits.x, xLimits.y);
        pos.z = Mathf.Clamp(pos.z, zLimits.x, zLimits.y);

        transform.position = pos;
    }
}
