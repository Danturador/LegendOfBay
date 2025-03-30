using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	[SerializeField] private GameObject bossHumanForm;
	[SerializeField] private GameObject bossDemonicForm;
	private bool isBossInactive;
	private void Awake()
	{
		bossHumanForm.SetActive(false);
		bossDemonicForm.SetActive(false);

		isBossInactive = true;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponent<PlayerController>() && isBossInactive)
		{
			isBossInactive = false;
			bossHumanForm.SetActive(true);
		}
	}
}
