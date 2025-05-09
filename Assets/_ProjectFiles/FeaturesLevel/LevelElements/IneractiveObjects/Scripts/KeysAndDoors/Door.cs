using System;
using System.Collections;
using UnityEngine;

namespace _ProjectFiles.FeaturesLevel.LevelElements.IneractiveObjects.Scripts.KeysAndDoors
{
	public class Door : MonoBehaviour
	{
		[SerializeField] private string doorID;
		[SerializeField] private Animation gatesOpenAnimation;
		[SerializeField] private AnimationClip gatesAnimationClip;
		[SerializeField] private BoxCollider2D doorCollider;
		public bool IsDoorsOpened { get; private set; }
		public string Id => Id;

		public event Action OnDoorOpened;
		
		//private void Awake() => IsDoorsOpened = false;

		private void OnDestroy()
		{
			OnDoorOpened = null;
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			if(other.gameObject.TryGetComponent(out Inventory inventory)
			   && inventory.GetKey(doorID) is not null)
				TryOpen(inventory.GetKey(doorID));
		}

		private bool TryOpen(Key key)
		{
			if (key != null)
			{
				if (key.keyID == doorID && !IsDoorsOpened)
				{
					StartCoroutine(Open());
					return true;
				}
				Debug.Log("���� ���� �� ��������.");
			}
			else
			{
				Debug.Log("���� �����������.");
			}
			return false;
		}

		private IEnumerator Open()
		{
			OnDoorOpened?.Invoke();
			EnviromentAudioInitializer.Instance.PlayGateOpenSound();
			gatesOpenAnimation.Play();
		
			yield return new WaitForSeconds(gatesAnimationClip.length);
		
			doorCollider.enabled = false;
			IsDoorsOpened = true;
		}

		public void SetState(bool enable)
		{
			if (enable)
				StartCoroutine(Open());
		}
	}
}