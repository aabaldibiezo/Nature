    using UnityEngine;
    using TMPro;

    public class Base : MonoBehaviour
    {
        public int aguaTotal = 0;
        public TMP_Text aguaText;

        [Header("Radio de depósito")]
        public float radioDeposito = 1.5f;

        public void DepositarAgua(int cantidad)
        {
            aguaTotal += cantidad;

            if (aguaText != null)
                aguaText.text = aguaTotal.ToString();

            Debug.Log("Agua total: " + aguaTotal);
        }

        // Gizmo para ver el radio en escena
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, radioDeposito);
        }
    }
