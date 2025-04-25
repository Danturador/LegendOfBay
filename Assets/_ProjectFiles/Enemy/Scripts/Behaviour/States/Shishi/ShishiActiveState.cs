using _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Shishi;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Shishi;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Windows.WebCam;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.States.Shishi
{
    public class ShishiActiveState : IEnterState, IExitState
    {
        public EnemyContainer _container;

        public ShishiActiveState(EnemyContainer container)
        {
            _container = container;
        }
        public void Enter()
        {
            Debug.Log("Shishi Active");
            _container.Navigation.Execute();
        }

        public void Exit()
        {
            Debug.Log("Shishi active exit");
            _container.Navigation.Stop();
        }
    }
}