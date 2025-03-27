using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmallAttackState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;
    private readonly int _currentComboCount;
    public PlayerSmallAttackState2(PlayerAnimationController playerAnimationController, int comboCount)
    {
        _playerAnimationController = playerAnimationController;
        _currentComboCount = comboCount;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerAttack);
        _playerAnimationController.SetInt(PlayerAnimationType.AttackNumber, _currentComboCount);
    }

    public override void OnStateExit()
    {

    }
}
