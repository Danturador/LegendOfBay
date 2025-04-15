using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovingItemForwardState2 : State2
{
     private readonly PlayerAnimationController _playerAnimationController;

    public PlayerMovingItemForwardState2(PlayerAnimationController playerAnimationController)
    {
         _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItemForward, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItemForward, false);
    }
}

