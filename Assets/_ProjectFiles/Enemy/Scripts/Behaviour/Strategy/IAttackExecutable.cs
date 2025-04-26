using System.Collections;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public interface IAttackExecutable
    {
        public IEnumerator Execute(Transform target);
        public void Stop();
    }
}