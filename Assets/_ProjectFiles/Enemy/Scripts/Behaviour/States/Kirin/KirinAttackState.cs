using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Kirin;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Kirin
{
    public class KirinAttackState : IEnterState, IExitState, IUpdateState
    {
        private readonly KirinAttackInfo _attackInfo;
        private readonly EnemyContainer _container;
        private float currentAttackDelay;

        public KirinAttackState(EnemyContainer container)
        {
            _container = container;
            _attackInfo = _container.Profile.AttackInfo as KirinAttackInfo;
        }

        public void Enter()
        {
            Debug.Log("attack state enter");
            currentAttackDelay = 0;
            _container.Animator.SetTrigger("attack");
            _container.Attack.Execute();
        }

        public void Exit()
        {
            Debug.Log("attack state exit");
            _container.Animator.SetTrigger("idle");
            _container.Attack.Stop();
        }

        public void Update()
        {
            currentAttackDelay += Time.deltaTime;

            if (currentAttackDelay >= _attackInfo.AttackDelay) currentAttackDelay = 0;
        }
    }
}