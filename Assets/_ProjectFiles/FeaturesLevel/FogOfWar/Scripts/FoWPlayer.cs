using System.Collections;
using UnityEngine;

public class FoWPlayer : MonoBehaviour
{
    public FogOfWarController fogOfWar;
    public Transform fogOfWarMark;

    [Range(0, 20)]
    public float sightDistance;
    public float checkInterval;

    private void Start()
    {
        StartCoroutine(CheckFogOfWar(checkInterval));
        fogOfWarMark.localScale = new Vector2(sightDistance, sightDistance) * 10f;
    }

    private IEnumerator CheckFogOfWar(float checkInterval)
    {
        while (true)
        {
            fogOfWar.MakeHole(transform.position, sightDistance);
            yield return new WaitForSeconds(checkInterval);
        }
    }
}