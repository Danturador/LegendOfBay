using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState2 : State2
{
    private readonly PlayerAnimationController _playerAnimationController;
    public PlayerDashState2(PlayerAnimationController playerAnimationController)
    {
        _playerAnimationController = playerAnimationController;
    }

    public override void OnStateEnter()
    {
        _playerAnimationController.SetTrigger(PlayerAnimationType.PlayerDash);
       
    }

    public override void OnStateExit()
    {
        
    }

}
