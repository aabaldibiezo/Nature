using UnityEngine;

public class Unit : MonoBehaviour
{
    protected bool isSelected = false;
    protected Renderer rend;
    public Color selectedColor = Color.green;
    protected Color defaultColor;

    public float moveSpeed = 5f;
    protected Vector3 targetPosition;
    protected bool isMoving = false;

    protected virtual void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            defaultColor = rend.material.color;
    }

    protected virtual void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Cuando llega al destino, detener el movimiento
            if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
                isMoving = false;
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.Instance.SelectUnit(this);
        }
    }

    public void Select()
    {
        isSelected = true;
        if (rend != null)
            rend.material.color = selectedColor;
    }

    public void Deselect()
    {
        isSelected = false;
        if (rend != null)
            rend.material.color = defaultColor;
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void MoveTo(Vector3 position)
    {
        // Hacer un raycast hacia abajo para ajustar la altura exacta del terreno
        RaycastHit hit;
        if (Physics.Raycast(position + Vector3.up * 10f, Vector3.down, out hit, 20f, LayerMask.GetMask("Ground")))
        {
            position.y = hit.point.y;
        }
        else
        {
            // Si no encuentra el suelo, mantener la altura actual
            position.y = transform.position.y;
        }

        targetPosition = position;
        isMoving = true;
    }
}
