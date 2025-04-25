using System.ComponentModel;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi
{
    public class ShishiAttackState : IEnterState, IExitState
    {
        public void Enter()
        {
            Debug.Log("Shishi attack");
        }

        public void Exit()
        {
            Debug.Log("Shishi attack exit");  
        }
    }
}