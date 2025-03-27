using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttackState : State2
{
    private readonly PlayerAnimationController _playerAnimationController;
    private readonly int _comboAttackCount;
    public ComboAttackState (PlayerAnimationController playerAnimationController, int comboAttackCount)
    {
        _playerAnimationController = playerAnimationController;
        _comboAttackCount = comboAttackCount;
    }

    public override void OnStateEnter()
    {
        switch (_comboAttackCount)
        {
            case 2:
                _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerComboAttack2);
                break;
            case 3:
                _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerComboAttack3);
                break;
            default:
                _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerComboAttack1);
                break;
        }
    }

    public override void OnStateExit()
    {

    }
}
