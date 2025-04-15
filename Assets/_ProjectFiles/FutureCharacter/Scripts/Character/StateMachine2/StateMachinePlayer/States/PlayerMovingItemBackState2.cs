using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingItemBackState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerMovingItemBackState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItemBack, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItemBack, false);
    }
}