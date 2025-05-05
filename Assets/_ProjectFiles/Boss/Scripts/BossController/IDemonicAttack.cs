using System.Collections;
using System;
using UnityEngine;

public interface IDemonicAttack
{
	public IEnumerator AttackPattern(Action<bool> setCascadeOfNeedles);
}
