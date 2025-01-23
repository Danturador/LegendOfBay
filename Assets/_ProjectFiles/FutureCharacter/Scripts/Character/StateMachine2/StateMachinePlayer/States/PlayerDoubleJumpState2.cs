using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJumpState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerDoubleJumpState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerDoubleJump, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerDoubleJump, false);
    }
}
