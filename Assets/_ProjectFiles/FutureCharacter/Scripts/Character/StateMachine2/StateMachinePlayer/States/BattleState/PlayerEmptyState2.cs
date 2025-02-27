using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmptyState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;
    public PlayerEmptyState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.EmptyState,true);
    }

    public override void OnStateExit()
    {
        _playerAnimationController.SetBool(PlayerAnimationType.EmptyState, false);
    }
}
