using System.Collections;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public abstract class INavigationExecutable
    {
        public abstract IEnumerator Execute(Transform target);
    }
}