using System.Collections;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public interface IAttackExecutable
    {
        public IEnumerator Execute(PlayerController target);
        public void Stop();
    }
}