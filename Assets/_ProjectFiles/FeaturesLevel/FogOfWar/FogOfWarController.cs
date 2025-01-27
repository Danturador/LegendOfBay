using UnityEngine;
using System;
using System.Collections;

public class FogOfWarController : MonoBehaviour
{
    [SerializeField] private Texture2D fogTexture;
    [SerializeField] private float revealRadius;
    [SerializeField] private Transform player;
    [SerializeField] private float updateInterval = 0.1f;
    [SerializeField] private int xADD;
    [SerializeField] private int yADD;

    private void Start()
    {
        Color[] pixels = fogTexture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.black;
        }
        fogTexture.SetPixels(pixels);
        fogTexture.Apply();
        StartCoroutine(UpdateFogOfWar());
    }

    private IEnumerator UpdateFogOfWar()
    {
        while (true)
        {
            RevealFog(player.position);
            yield return new WaitForSeconds(updateInterval);
        }
    }

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.color = Color.yellow; // Color of the circle
            Gizmos.DrawWireSphere(player.position, revealRadius); // Draw circle
        }
    }

    private void RevealFog(Vector2 position)
    {
        // Calculate the center based on the texture size
        int textureCenterX = fogTexture.width / 2;
        int textureCenterY = fogTexture.height / 2;

        int x = Mathf.FloorToInt(position.x + xADD + textureCenterX);
        int y = Mathf.FloorToInt(position.y + yADD + textureCenterY);
        Debug.Log($"position.x: {position.x}, loc position.x: {position.x}, x: {x} / {fogTexture.width}, y: {y} / {fogTexture.height}");
        int radius = Mathf.FloorToInt(revealRadius);

        for (int i = -radius; i < radius; i++)
        {
            for (int j = -radius; j < radius; j++)
            {
                if (i * i + j * j <= radius * radius)
                {
                    int pixelX = x + i;
                    int pixelY = y + j;

                    if (pixelX >= 0 && pixelX < fogTexture.width && pixelY >= 0 && pixelY < fogTexture.height)
                    {
                        fogTexture.SetPixel(pixelX, pixelY, Color.clear);
                    }
                }
            }
        }
        fogTexture.Apply();
    }
}