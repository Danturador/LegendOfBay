using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
	[SerializeField] private EnviromentAudioInitializer soundManager;
	[SerializeField] private TriggerType triggerType;
	private bool isPlayerInside;
	private bool isNewAmbientPlay;
	private enum TriggerType 
	{
		Transition, 
		Cave
	}
	private void Awake()
	{
		isPlayerInside = false;
		isNewAmbientPlay = false;
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
		switch(triggerType)
		{
			case TriggerType.Cave:
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
				break;
			case TriggerType.Transition:
				if (isNewAmbientPlay)
				{
					soundManager.PlayAmbientEnd();
					isNewAmbientPlay = false;
				}
				else
				{
					isNewAmbientPlay = true;
				}
				break;
		}
	}
}
