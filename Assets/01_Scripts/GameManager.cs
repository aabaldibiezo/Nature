using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public LayerMask groundLayer;

    private Unit selectedUnit;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        HandleUnitSelection();
        HandleRightClickAction();
    }

    void HandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null)
                    SelectUnit(unit);
            }
        }
    }

    void HandleRightClickAction()
    {
        if (selectedUnit == null) return;

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Usamos RaycastAll para obtener todos los colliders en la línea del rayo
            RaycastHit[] hits = Physics.RaycastAll(ray, 100f);
            if (hits.Length == 0) return;

            // Ordenamos por distancia al origen del rayo (para procesar del más cercano al más lejano)
            System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            // Variables para almacenar el primer recurso/base/ground encontrados
            WaterResource foundResource = null;
            Base foundBase = null;
            RaycastHit? groundHit = null;

            foreach (var hit in hits)
            {
                // debug rápido: ver qué golpeamos
                Debug.Log($"RaycastAll golp. {hit.collider.name} | Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)} | Dist: {hit.distance}");

                // Revisar recurso (en el collider o en el padre)
                var recurso = hit.collider.GetComponent<WaterResource>() ?? hit.collider.GetComponentInParent<WaterResource>();
                if (recurso != null)
                {
                    foundResource = recurso;
                    // Tomamos el primero (el más cercano) y salimos del foreach
                    break;
                }

                // Revisar base
                var baseObj = hit.collider.GetComponent<Base>() ?? hit.collider.GetComponentInParent<Base>();
                if (baseObj != null)
                {
                    foundBase = baseObj;
                    // no break: preferimos aún recursos si aparecen más cerca en la lista
                    break;
                }

                // Si es layer ground, guardamos el hit como candidato
                if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
                {
                    groundHit = hit;
                    // no break, porque puede que haya un recurso por encima/encima del ground en la lista
                }
            }

            // Si encontramos recurso, asignarlo
            if (foundResource != null && selectedUnit is Collector collector)
            {
                Debug.Log("Asignando recurso detectado: " + foundResource.name);
                collector.AsignarRecurso(foundResource);
                return;
            }

            // Si encontramos base, asignarla
            if (foundBase != null && selectedUnit is Collector collector2)
            {
                Debug.Log("Asignando base detectada: " + foundBase.name);
                collector2.basePrincipal = foundBase;
                return;
            }

            // Si no hay recurso ni base, pero golpeamos ground, mover a ese punto
            if (groundHit.HasValue)
            {
                Debug.Log("Cliqueo en suelo en punto: " + groundHit.Value.point);
                selectedUnit.MoveTo(groundHit.Value.point);
                return;
            }

            // Si no hubo ground ni recurso ni base, movemos al punto del primer hit (por seguridad)
            selectedUnit.MoveTo(hits[0].point);
        }
    }


    public void SelectUnit(Unit unit)
    {
        if (selectedUnit != null)
            selectedUnit.Deselect();

        selectedUnit = unit;
        selectedUnit.Select();
    }

    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
