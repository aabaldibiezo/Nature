using UnityEngine;
using UnityEngine.UI;

public class Tienda : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelTienda;
    public Button botonTienda;
    public Button botonComprarRecolector;

    [Header("Spawn del Recolector")]
    public GameObject prefabRecolector;     
    public Transform puntoDeSpawn;        

    void Start()
    {
        
        panelTienda.SetActive(false);

        
        botonTienda.onClick.AddListener(TogglePanelTienda);
        botonComprarRecolector.onClick.AddListener(ComprarRecolector);
    }

    void TogglePanelTienda()
    {
        
        panelTienda.SetActive(!panelTienda.activeSelf);
    }

    void ComprarRecolector()
    {
        
        if (prefabRecolector == null)
        {
            Debug.LogWarning("⚠️ No se asignó el prefab del recolector en el inspector.");
            return;
        }

        
        Vector3 spawnPos = puntoDeSpawn ? puntoDeSpawn.position : Vector3.zero;

        
        Instantiate(prefabRecolector, spawnPos, Quaternion.identity);

        Debug.Log("🧍‍♂️ Recolector comprado e instanciado correctamente.");
    }
}
