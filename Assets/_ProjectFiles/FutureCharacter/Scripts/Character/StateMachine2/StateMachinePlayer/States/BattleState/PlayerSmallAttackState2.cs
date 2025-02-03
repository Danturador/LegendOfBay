using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSmallAttackState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;
    public PlayerSmallAttackState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerSmallAttack);
    }

    public override void OnStateExit()
    {

    }
}
