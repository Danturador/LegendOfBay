using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingItemState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;

    public PlayerMovingItemState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItem, true);
    }

    public override void OnStateExit()
    {
      //  _playerAnimationController.SetBool(PlayerAnimationType.PlayerMovingItemStay, true);
    }
}
