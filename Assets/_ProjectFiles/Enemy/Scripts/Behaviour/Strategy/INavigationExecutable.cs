using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public interface INavigationExecutable
    {
        public IEnumerator Execute(PlayerController target);
        public void Stop();
    }
}