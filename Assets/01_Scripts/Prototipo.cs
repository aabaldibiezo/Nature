using UnityEngine;

public class Prototipo : MonoBehaviour
{
    public float velocidad = 5f; 
    private Rigidbody rb;

    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
       
        float moverX = Input.GetAxisRaw("Horizontal");
        float moverZ = Input.GetAxisRaw("Vertical");

        
        Vector3 movimiento = new Vector3(moverX, 0f, moverZ).normalized;

        
        rb.MovePosition(transform.position + movimiento * velocidad * Time.deltaTime);

        
        if (movimiento != Vector3.zero)
        {
            Quaternion rotacion = Quaternion.LookRotation(movimiento);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, 0.15f);
        }
    }
}
