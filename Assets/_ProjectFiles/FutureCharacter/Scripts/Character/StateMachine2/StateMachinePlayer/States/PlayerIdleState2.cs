using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerIdleState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerIdle, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerIdle, false);
    }
}
