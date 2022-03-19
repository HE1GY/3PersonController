using UnityEngine;

namespace ShitPalka
{
    public class LegAnimation
    {
        private Animator _animator;
        private static readonly int _moveHash = Animator.StringToHash("Move");

        public LegAnimation(Animator animator)
        {
            _animator = animator;
        }

        public void PlayLegMove()
        {
            _animator.SetTrigger(_moveHash);
        }

        public void TurnAnimator(bool on)
        {
            _animator.enabled = on;
        }
    }
}