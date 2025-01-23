using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerJumpState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerJump, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerJump, false);
    }
}
