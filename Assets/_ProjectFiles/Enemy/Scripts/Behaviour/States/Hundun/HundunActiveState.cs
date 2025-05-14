using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;

public class HundunActiveState : IEnterState, IExitState
{
    public EnemyContainer Container { get; set; }

    public void Enter()
    {
        Debug.Log("Hundun Active");
        Container.Navigation.Execute();
    }

    public void Exit()
    {
        Container.Navigation.Stop();
    }
}