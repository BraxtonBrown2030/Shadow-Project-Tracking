using UnityEngine;

public class SpawningScript : MonoBehaviour
{
    public GameObject prefab;
    public GameObject target; // Target GameObject
    public float radius = 5f; // Radius around the target
    public int segments = 100; // Smoothness of the circle
    public Color lineColor = Color.green;

    private LineRenderer lineRenderer;

    void Start()
    {
        if (target == null) return;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = lineColor;
        lineRenderer.endColor = lineColor;

        DrawRadius();
    }

    void DrawRadius()
    {
        float angleStep = 360f / segments;
        for (int i = 0; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 pointPosition = target.transform.position + new Vector3(
                Mathf.Cos(angle) * radius,
                0,
                Mathf.Sin(angle) * radius
            );
            lineRenderer.SetPosition(i, pointPosition);
            
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnAtPosition(target.transform.position);
        }
    }

    void SpawnAtPosition(Vector3 position)
    {
        Vector3 randomPointInsideCircle = target.transform.position + new Vector3(
            Mathf.Cos(Random.Range(0, 360) * Mathf.Deg2Rad) * radius * Random.value,
            0,
            Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad) * radius * Random.value
        );

        Instantiate(prefab, randomPointInsideCircle, Quaternion.identity);
    }
}
