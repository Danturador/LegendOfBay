using System.Collections;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public interface INavigationExecutable
    {
        protected bool IsActive { get; set; }
        public IEnumerator Execute(Transform target);
        public void Stop();
    }
}