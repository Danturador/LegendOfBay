using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingItemCancelState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerMovingItemCancelState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItemCancel, true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItemCancel, false);
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItem,false);
    }
}