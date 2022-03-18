using UnityEngine;

namespace ShitPalka
{
    public class PalkaAnimation
    {
        private const string Leg1MoveAnimation = "1LegMove";
        private const string Leg2MoveAnimation = "2LegMove";
        private const string Leg3MoveAnimation = "3LegMove";
        private const string Leg4MoveAnimation = "4LegMove";

        private Animator _animator;

        public PalkaAnimation(Animator animator)
        {
            _animator = animator;
        }
        

        public void Play1LegAnimation()
        {
            _animator.Play(Leg1MoveAnimation);
        }
        public void Play2LegAnimation()
        {
            /*_animator.Play(Leg2MoveAnimation);*/
        }

        public void Play3LegAnimation()
        {
            /*_animator.Play(Leg3MoveAnimation);*/
        }

        public void Play4LegAnimation()
        {
            /*_animator.Play(Leg4MoveAnimation);*/
        }
        
    }
}