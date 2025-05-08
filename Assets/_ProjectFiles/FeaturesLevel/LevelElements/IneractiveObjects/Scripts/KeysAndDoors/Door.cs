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
		private void Awake() => IsDoorsOpened = false;
		public bool TryOpen(Key key)
		{
			if (key != null)
			{
				if (key.keyID == doorID && !IsDoorsOpened)
				{
					StartCoroutine(Open());
					return true;
				}
				else
				{
					Debug.Log("���� ���� �� ��������.");
				}
			}
			else
			{
				Debug.Log("���� �����������.");
			}
			return false;
		}

		private IEnumerator Open()
		{
			EnviromentAudioInitializer.Instance.PlayGateOpenSound();
			gatesOpenAnimation.Play();
		
			yield return new WaitForSeconds(gatesAnimationClip.length);
		
			doorCollider.enabled = false;
			IsDoorsOpened = true;
		}
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.gameObject.TryGetComponent(out Inventory inventory))
				TryOpen(inventory.GetKey(doorID));
		}

		public void SetState(bool enable)
		{
			IsDoorsOpened = enable;
		}
	}
}