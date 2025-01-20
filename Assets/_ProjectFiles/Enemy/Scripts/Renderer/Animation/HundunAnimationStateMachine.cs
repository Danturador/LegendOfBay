using Spine;
using Spine.Unity;
using UnityEngine;
using AnimationState = Spine.AnimationState;

public class HundunAnimationStateMachine : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SpineAnimation] [SerializeField] private string idleAnimationName;
    private Skeleton skeleton;
    private AnimationState spineAnimationState;

    private void Start()
    {
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;

        spineAnimationState.SetAnimation(0, idleAnimationName, true);
    }
}