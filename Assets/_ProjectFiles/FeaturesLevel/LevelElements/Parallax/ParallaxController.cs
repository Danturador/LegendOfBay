using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class ParallaxController : MonoBehaviour
{
    public GameObject cam;
    public float defaultParallaxEffect = 0.5f;

    private List<float> startPositions = new List<float>();
    private List<float> lengths = new List<float>();
    private List<TilemapRenderer> tilemapRenderers = new List<TilemapRenderer>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            TilemapRenderer tilemapRenderer = child.GetComponent<TilemapRenderer>();
            if (tilemapRenderer != null)
            {
                startPositions.Add(child.position.x);
                lengths.Add(tilemapRenderer.bounds.size.x);
                tilemapRenderers.Add(tilemapRenderer);
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < tilemapRenderers.Count; i++)
        {
            Transform child = transform.GetChild(i);
            float zPosition = child.position.z;
            float parallaxEffect = CalculateParallaxEffect(zPosition);

            float startpos = startPositions[i];
            float length = lengths[i];

            float temp = (cam.transform.position.x * (1 - parallaxEffect));
            float dist = (cam.transform.position.x * parallaxEffect);

            Vector3 newPosition = new Vector3(startpos + dist, child.position.y, child.position.z);

            if (!float.IsInfinity(newPosition.x) && !float.IsNaN(newPosition.x))
            {
                child.position = newPosition;
            }

            if (temp > startpos + length)
                startPositions[i] += length;
            else if (temp < startpos - length)
                startPositions[i] -= length;
        }
    }
    private float CalculateParallaxEffect(float zPosition)
    {
        return defaultParallaxEffect / (zPosition + 1);
    }
}