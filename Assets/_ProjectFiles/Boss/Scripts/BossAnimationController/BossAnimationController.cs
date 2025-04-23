using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimationController<T> where T : Enum
{
	private Animator _animator;
	private Dictionary<T, int> hashStorage = new Dictionary<T, int>();

	public BossAnimationController(Animator animator)
	{
		_animator = animator;
		foreach (T animationType in Enum.GetValues(typeof(T)))
		{
			hashStorage.Add(animationType, Animator.StringToHash(animationType.ToString()));
		}
	}

	public void SetBool(T animationType, bool value)
	{
		_animator.SetBool(hashStorage[animationType], value);
	}

	public void SetFloat(T animationType, float value)
	{
		_animator.SetFloat(hashStorage[animationType], value);
	}

	public void SetPlay(T animationType)
	{
		_animator.Play(hashStorage[animationType]);
	}

	public void SetTrigger(T animationType)
	{
		_animator.SetTrigger(hashStorage[animationType]);
	}
}

public class HumanFormAnimationController : BossAnimationController<HumanFormAnimationType>
{
	public HumanFormAnimationController(Animator animator) : base(animator) { }
}

public class DemonicFormAnimationController : BossAnimationController<DemonicFormAnimationType>
{
	public DemonicFormAnimationController(Animator animator) : base(animator) { }
}
