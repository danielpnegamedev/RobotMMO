using UnityEngine;

public class DrawLineCircle : MonoBehaviour
{
    [Header("Line Configs")]
    public float radius = 5f;
    public int segments = 50;
    public Color lineColor = Color.red;
    private LineRenderer line;


    public void OnEnable()
    {
        line = gameObject.AddComponent<LineRenderer>();
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.loop = true;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = lineColor;
        line.endColor = lineColor;

        DrawCircle();
    }

    void DrawCircle()
    {
        float angle = 0f;
        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, 0, y));

            angle += (360f / segments);
        }
    }
}
