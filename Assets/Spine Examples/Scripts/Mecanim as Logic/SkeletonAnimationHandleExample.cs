using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity.Examples
{
    public class SkeletonAnimationHandleExample : MonoBehaviour
    {
        public SkeletonAnimation skeletonAnimation;
        public List<StateNameToAnimationReference> statesAndAnimations = new();

        public List<AnimationTransition> transitions = new();

        public Animation TargetAnimation { get; private set; }

        private void Awake()
        {
            foreach (var entry in statesAndAnimations) entry.animation.Initialize();
            foreach (var entry in transitions)
            {
                entry.from.Initialize();
                entry.to.Initialize();
                entry.transition.Initialize();
            }
        }

        public void SetFlip(float horizontal)
        {
            if (horizontal != 0) skeletonAnimation.Skeleton.ScaleX = horizontal > 0 ? 1f : -1f;
        }

        public void PlayAnimationForState(string stateShortName, int layerIndex)
        {
            PlayAnimationForState(StringToHash(stateShortName), layerIndex);
        }

        public void PlayAnimationForState(int shortNameHash, int layerIndex)
        {
            var foundAnimation = GetAnimationForState(shortNameHash);
            if (foundAnimation == null)
                return;

            PlayNewAnimation(foundAnimation, layerIndex);
        }

        public Animation GetAnimationForState(string stateShortName)
        {
            return GetAnimationForState(StringToHash(stateShortName));
        }

        public Animation GetAnimationForState(int shortNameHash)
        {
            var foundState = statesAndAnimations.Find(entry => StringToHash(entry.stateName) == shortNameHash);
            return foundState == null ? null : foundState.animation;
        }

        public void PlayNewAnimation(Animation target, int layerIndex)
        {
            Animation transition = null;
            Animation current = null;

            current = GetCurrentAnimation(layerIndex);
            if (current != null)
                transition = TryGetTransition(current, target);

            if (transition != null)
            {
                skeletonAnimation.AnimationState.SetAnimation(layerIndex, transition, false);
                skeletonAnimation.AnimationState.AddAnimation(layerIndex, target, true, 0f);
            }
            else
            {
                skeletonAnimation.AnimationState.SetAnimation(layerIndex, target, true);
            }

            TargetAnimation = target;
        }

        public void PlayOneShot(Animation oneShot, int layerIndex)
        {
            var state = skeletonAnimation.AnimationState;
            state.SetAnimation(0, oneShot, false);

            var transition = TryGetTransition(oneShot, TargetAnimation);
            if (transition != null)
                state.AddAnimation(0, transition, false, 0f);

            state.AddAnimation(0, TargetAnimation, true, 0f);
        }

        private Animation TryGetTransition(Animation from, Animation to)
        {
            foreach (var transition in transitions)
                if (transition.from.Animation == from && transition.to.Animation == to)
                    return transition.transition.Animation;
            return null;
        }

        private Animation GetCurrentAnimation(int layerIndex)
        {
            var currentTrackEntry = skeletonAnimation.AnimationState.GetCurrent(layerIndex);
            return currentTrackEntry != null ? currentTrackEntry.Animation : null;
        }

        private int StringToHash(string s)
        {
            return Animator.StringToHash(s);
        }

        [Serializable]
        public class StateNameToAnimationReference
        {
            public string stateName;
            public AnimationReferenceAsset animation;
        }

        [Serializable]
        public class AnimationTransition
        {
            public AnimationReferenceAsset from;
            public AnimationReferenceAsset to;
            public AnimationReferenceAsset transition;
        }
    }
}