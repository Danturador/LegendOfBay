using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Dictionary<PlayerAnimationType, int> hashStorage = new Dictionary<PlayerAnimationType, int>();

    //public Animator Animator => _animator;

    public PlayerAnimationController(Animator animator)
    {
        _animator = animator;
        foreach (PlayerAnimationType paType in Enum.GetValues(typeof(PlayerAnimationType)))
        {
            hashStorage.Add(paType, Animator.StringToHash(paType.ToString()));
        }
    }

    public void SetBool(PlayerAnimationType animationType, bool value)
    {
        _animator.SetBool(hashStorage[animationType], value);
    }

    public void SetFloat(PlayerAnimationType animationType, float value)
    {
        _animator.SetFloat(hashStorage[animationType], value);
    }

    public void SetInt(PlayerAnimationType animationType, int value)
    {
        _animator.SetInteger(hashStorage[animationType], value);
    }
    public void SetPlay(PlayerAnimationType characterAnimationType)
    {
        _animator.Play((hashStorage[characterAnimationType]));
    }

    public void SetTrigger(PlayerAnimationType characterAnimationType)
    {
        _animator.SetTrigger((hashStorage[characterAnimationType]));
    }
}
