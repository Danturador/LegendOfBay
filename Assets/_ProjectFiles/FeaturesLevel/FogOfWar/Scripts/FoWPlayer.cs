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
		//StartCoroutine(CheckFogOfWar());
		_ = CheckFogOfWar();
	}

	//private IEnumerator CheckFogOfWar()
	private async Task CheckFogOfWar()
	{
		while (true)
		{
			//fogOfWar.MakeHole(transform.position, sightDistance);
			//yield return null;
			await fogOfWar.MakeHole(transform.position, sightDistance);
			await Task.Delay((int)(checkInterval * 1000));
		}
	}
}