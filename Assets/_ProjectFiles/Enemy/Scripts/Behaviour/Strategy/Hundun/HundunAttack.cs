using System;
using System.Collections;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public class HundunAttack : IAttackExecutable
    {

        public IEnumerator Execute(PlayerController target)
        {
            yield return null;
        }

        public void Stop()
        {
            
        }
    }
}