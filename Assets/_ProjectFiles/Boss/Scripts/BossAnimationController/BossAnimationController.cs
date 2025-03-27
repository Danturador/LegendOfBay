using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Dictionary<BossAnimationType, int> hashStorage = new Dictionary<BossAnimationType, int>();

	public BossAnimationController(Animator animator)
	{
		_animator = animator;
		foreach (BossAnimationType baType in Enum.GetValues(typeof(BossAnimationType)))
		{
			hashStorage.Add(baType, Animator.StringToHash(baType.ToString()));
		}
	}

	public void SetBool(BossAnimationType animationType, bool value)
	{
		_animator.SetBool(hashStorage[animationType], value);
	}

	public void SetFloat(BossAnimationType animationType, float value)
	{
		_animator.SetFloat(hashStorage[animationType], value);
	}

	public void SetPlay(BossAnimationType characterAnimationType)
	{
		_animator.Play((hashStorage[characterAnimationType]));
	}

	public void SetTrigger(BossAnimationType characterAnimationType)
	{
		_animator.SetTrigger((hashStorage[characterAnimationType]));
	}
}
