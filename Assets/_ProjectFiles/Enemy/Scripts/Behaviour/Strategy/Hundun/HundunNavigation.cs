using System.Collections;
using System.Threading;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Hundun;
using _ProjectFiles.SoundContainer;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy
{
    public class HundunNavigation : INavigationExecutable
    {
        private readonly EnemyContainer _container;
        private readonly HundunNavigationInfo _info;
        private readonly Rigidbody2D _rigidbody;
        private readonly AnimationCurve _speedCurve;
        private CancellationTokenSource _token;

        public HundunNavigation(EnemyContainer container, HundunNavigationInfo info)
        {
            _container = container;
            _rigidbody = container.Rigidbody;
            _info = info;
            _speedCurve = info.SpeedCurve;
        }

        public IEnumerator Execute(PlayerController target)
        {
            _token = new CancellationTokenSource();
            yield return new WaitForSeconds(_info.StartDashDelay);

            while (!_token.IsCancellationRequested)
            {
                var randomOffsetDirection = Random.Range(0, 2) == 1 ? -1 : 1;
                Vector2 currentTargetPosition = target.transform.position + new Vector3(1, 1, 0) *
                    Random.Range(_info.RandomTargetOffset.x, _info.RandomTargetOffset.y) * randomOffsetDirection;
                Vector2 moveDirection = (target.transform.position - _rigidbody.transform.position).normalized;
                var moveTime = _info.DashTime;
                var distanceToTarget = Vector2.Distance(currentTargetPosition, _rigidbody.transform.position);
                var velocityMagnitude = distanceToTarget / _speedCurve.FunctionSquare(100);
                var time = 0f;
                _container.Renderer.CurrentScale = -moveDirection.x / Mathf.Abs(moveDirection.x);
                _container.Audio.PlaySoundEffect(SoundType.HundunActive);

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

        public void Stop()
        {
            _token.Cancel();
            _container.Navigation.StopAllCoroutines();
            _rigidbody.velocity = Vector2.zero;
        }

        public IEnumerator SendToPoint(Vector3 point)
        {
            Vector2 moveDirection = (point - _rigidbody.transform.position).normalized;
            var moveTime = _info.DashTime;
            var distanceToTarget = Vector2.Distance(point, _rigidbody.transform.position);
            var velocityMagnitude = distanceToTarget / _speedCurve.FunctionSquare(100);
            var time = 0f;

            while (time < moveTime)
            {
                time += Time.deltaTime;

                var currentVelocity = velocityMagnitude * _speedCurve.Evaluate(time) * moveDirection;
                _rigidbody.velocity = currentVelocity;

                yield return null;
            }
        }
    }
}