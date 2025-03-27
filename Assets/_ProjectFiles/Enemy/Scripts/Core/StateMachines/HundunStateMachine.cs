namespace _ProjectFiles.Enemy.Scripts.Core.StateMachines
{
    public class HundunStateMachine : StateMachine
    {
        public HundunStateMachine(EnemyContainer container, EnemyProfile profile) : base(container, profile)
        {
        }

        protected override (IState[] states, Transition[] transitions) SetMachineBehaviour()
        {
            var passiveState = new HundunPassiveState();
            var activeState = new HundunActiveState(_container);
            var attackState = new HundunAttackState();
            
            var states = new IState[] { passiveState, activeState, attackState };
            
            var transitions = new Transition[]
            {
                new(typeof(HundunPassiveState), typeof(HundunActiveState), activeState.IsInPlayerRange),
                new(typeof(HundunActiveState), typeof(HundunPassiveState), activeState.IsOutOfPlayerRange),
                new(typeof(HundunActiveState), typeof(HundunAttackState), attackState.CanAttack)
            };
            
            return (states, transitions);
        }
    }
}