using System.Collections;
using System.Numerics;
using _ProjectFiles.Enemy.Scripts.Core;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public class HundunNavigation : INavigationExecutable
    {
        private readonly EnemyNavigationInfo _info;
        private readonly Rigidbody2D _rigidbody;
        private readonly AnimationCurve _speedCurve;

        public HundunNavigation(Rigidbody2D rigidbody, EnemyNavigationInfo info, AnimationCurve speedCurve)
        {
            _rigidbody = rigidbody;
            _info = info;
            _speedCurve = speedCurve;
        }

        public override IEnumerator Execute(Transform target)
        {
            yield return new WaitForSeconds(_info.StartDashDelay);

            while (true)
            {
                int randomOffsetDirection = Random.Range(0, 2) == 1 ? -1 : 1;
                Vector2 currentTargetPosition = target.position + (new Vector3(1, 1, 0)) * Random.Range(_info.RandomTargetOffset.x, _info.RandomTargetOffset.y) * randomOffsetDirection;
                Vector2 moveDirection = (target.transform.position - _rigidbody.transform.position).normalized;
                var moveTime = _info.DashTime;
                var distanceToTarget = Vector2.Distance(currentTargetPosition, _rigidbody.transform.position);
                var velocityMagnitude = distanceToTarget / _speedCurve.FunctionSquare(100);
                var time = 0f;

                while (time < moveTime)
                {
                    time += Time.deltaTime;

                    var currentVelocity = velocityMagnitude * _speedCurve.Evaluate(time) * moveDirection;
                    _rigidbody.velocity = currentVelocity;

                    yield return null;
                }

                yield return new WaitForSeconds(_info.DashTimeInterval);
            }
        }
    }
}