using UnityEngine;
using UnityEditor;

public class AlignPolygonPoints : MonoBehaviour
{
    [SerializeField] private float error = 0.3f;
    public void AlignPoints()
    {
        PolygonCollider2D[] colliders = GetComponentsInChildren<PolygonCollider2D>();

        foreach (var polygonCollider in colliders)
        {
            Vector2[] points = polygonCollider.points;

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    if (Mathf.Abs(points[i].y - points[j].y) <= error)
                    {
                        float roundedY = (float)System.Math.Round(points[i].y, 2);
                        points[i].y = roundedY;
                        points[j].y = roundedY;
                    }

                    if (Mathf.Abs(points[i].x - points[j].x) <= error)
                    {
                        float roundedX = (float)System.Math.Round(points[i].x, 2);
                        points[i].x = roundedX;
                        points[j].x = roundedX;
                    }
                }
            }

            polygonCollider.points = points;
        }
    }
}