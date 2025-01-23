using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerRunState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerRun, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerRun, false);
    }
}
