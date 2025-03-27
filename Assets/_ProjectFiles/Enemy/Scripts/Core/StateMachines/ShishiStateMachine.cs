using _ProjectFiles.Enemy.Scripts.Core.Shishi;

namespace _ProjectFiles.Enemy.Scripts.Core.StateMachines
{
    public class ShishiStateMachine : StateMachine
    {
        public ShishiStateMachine(EnemyContainer container, EnemyProfile profile) : base(container, profile)
        {
        }

        protected override (IState[] states, Transition[] transitions) SetMachineBehaviour()
        {
            var passiveState = new ShishiPassiveState();
            var activeState = new ShishiActiveState(_container, _profile.NavigationInfo as ShishiNavigationInfo);
            var attackState = new ShishiAttackState(_container);
            
            var states = new IState[] { passiveState, activeState, attackState };
            
            var transitions = new Transition[]
            {
                new(typeof(ShishiPassiveState), typeof(ShishiAttackState), attackState.CanAttack),
                new(typeof(ShishiAttackState), typeof(ShishiPassiveState), attackState.CannotAttack),
                new(typeof(ShishiAttackState), typeof(ShishiActiveState), activeState.IsInEscapeRange),
                new(typeof(ShishiActiveState), typeof(ShishiAttackState), activeState.IsOutOfEscapeRange),
            };
            
            return (states, transitions);
        }
    }
}