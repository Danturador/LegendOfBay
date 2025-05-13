using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using _ProjectFiles.SaveSystem;
using static _ProjectFiles.SoundContainer.SoundType;

public class AudioTrigger : MonoBehaviour
{
	[Inject] private SaveSystemController _saveSystemController;
	[SerializeField] private EnviromentAudioInitializer soundManager;
	[SerializeField] private TriggerType triggerType;
	[SerializeField] private bool needHandleColliderExit;
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
		if (collision.GetComponent<PlayerController>() != null && needHandleColliderExit)
			if (collision.GetComponent<PlayerController>() != null)
			{
				PlayAmbientByType();
			}
	}
	private void PlayAmbientByType()
	{
		switch (triggerType)
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
					soundManager.PlayAmbient(AmbientStart);
					_saveSystemController.UpdateCurrentAmbient(AmbientStart);
					isNewAmbientPlay = false;
				}
				else
				{
					soundManager.PlayAmbient(AmbientEnd);
					_saveSystemController.UpdateCurrentAmbient(AmbientEnd);
					isNewAmbientPlay = true;
				}
				break;
		}
	}
}