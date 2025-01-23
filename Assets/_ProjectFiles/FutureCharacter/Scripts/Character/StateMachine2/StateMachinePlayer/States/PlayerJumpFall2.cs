using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpFall2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerJumpFall2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerFall, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerFall, false);
    }
}
