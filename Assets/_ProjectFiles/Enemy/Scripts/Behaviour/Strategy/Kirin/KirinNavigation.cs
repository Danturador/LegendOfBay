using System.Collections;
using _ProjectFiles.Enemy.Scripts.Core;
using _ProjectFiles.Enemy.Scripts.Core.Instances.Kirin;
using UnityEngine;

namespace _ProjectFiles.Enemy.Scripts.Behaviour.Strategy.Kirin
{
    public class KirinNavigation : INavigationExecutable
    {
        private readonly KirinAttackInfo _attackInfo;
        private readonly EnemyContainer _container;
        private readonly KirinNavigationInfo _naviInfo;
        private readonly AnimationCurve _speedCurve;

        public KirinNavigation(EnemyContainer container)
        {
            _container = container;
            _naviInfo = container.Profile.NavigationInfo as KirinNavigationInfo;
            _attackInfo = container.Profile.AttackInfo as KirinAttackInfo;
            _speedCurve = _naviInfo.SpeedCurve;
        }

        public bool IsPreparing { get; private set; }

        public IEnumerator Execute(PlayerController target)
        {
            while (true)
            {
                _container.Animator.SetTrigger("prepare");
                _container.Renderer.CurrentScale =
                    target.transform.position.x < _container.transform.position.x ? -1 : 1;

                IsPreparing = true;
                yield return new WaitForSeconds(_naviInfo.StartDashDelay);
                IsPreparing = false;
                _container.Animator.SetBool("active", true);

                var targetDelta = target.transform.position.x - _container.Rigidbody.transform.position.x;
                var moveDirection = (int)(targetDelta / Mathf.Abs(targetDelta));
                var moveTime = _naviInfo.DashTime;
                var velocityMagnitude = Mathf.Abs(targetDelta) / _speedCurve.FunctionSquare(100);
                var time = 0f;

                if (Mathf.Abs(targetDelta) < _attackInfo.AttackRange)
                {
                    _container.Animator.SetBool("active", false);
                    yield return new WaitForSeconds(_naviInfo.DashTimeInterval);
                }
                else
                {
                    while (time < moveTime)
                    {
                        time += Time.deltaTime;

                        var currentVelocity =
                            velocityMagnitude * _speedCurve.Evaluate(time) * new Vector2(moveDirection, 0);
                        _container.Rigidbody.velocity = currentVelocity;

                        if (currentVelocity == Vector2.zero) break;
                        yield return null;
                    }

                    _container.Animator.SetBool("active", false);
                    yield return new WaitForSeconds(_naviInfo.DashTimeInterval);
                }
            }
        }

        public void Stop()
        {
            _container.Rigidbody.velocity = Vector2.zero;
            _container.Animator.SetBool("active", false);
            _container.Navigation.StopAllCoroutines();
        }
    }
}