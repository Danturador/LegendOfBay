using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrapplingHookState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerGrapplingHookState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerGrapplingHook);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerJump, true);
    }
}
