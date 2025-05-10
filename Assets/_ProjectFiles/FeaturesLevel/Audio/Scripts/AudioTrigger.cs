using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
	[SerializeField] private EnviromentAudioInitializer soundManager;
	private bool isPlayerInside;
	private void Awake()
	{
		isPlayerInside = false;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			PlayAmbientByType();
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() != null)
		{
			PlayAmbientByType();
		}
	}
	private void PlayAmbientByType()
	{
				if (isPlayerInside)
				{
					soundManager.ExitCave();
					isPlayerInside = false;		
				}
				else
				{
					soundManager.EnterCave();
					isPlayerInside = true;		
				}
	}
}
