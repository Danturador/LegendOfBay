using System.Collections;
using System.Threading.Tasks;
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
		fogOfWarMark.localScale = new Vector2(sightDistance, sightDistance) * 10f;
		StartCoroutine(CheckFogOfWar(checkInterval));
	}

	private IEnumerator CheckFogOfWar(float checkInterval)
	{
		while (true)
		{
			_ = MakeHoleAsync(transform.position, sightDistance);

			yield return new WaitForSeconds(checkInterval);
		}
	}

	private async Task MakeHoleAsync(Vector2 position, float holeRadius)
	{
		await Task.Run(() => fogOfWar.MakeHole(position, holeRadius));
	}
}