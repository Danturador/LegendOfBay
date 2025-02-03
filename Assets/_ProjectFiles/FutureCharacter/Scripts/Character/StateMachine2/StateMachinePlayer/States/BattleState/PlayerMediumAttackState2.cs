using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMediumAttackState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;
    public PlayerMediumAttackState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerMediumAttack);

    }

    public override void OnStateExit()
    {

    }
}
