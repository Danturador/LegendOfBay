using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerLandingState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerLanding, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerLanding, false);
    }
}
