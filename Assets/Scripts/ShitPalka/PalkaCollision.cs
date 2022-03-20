using System;
using UnityEngine;

namespace ShitPalka
{
    public class PalkaCollision
    {
        public event Action Hit;

        private CharacterController _characterController;

        public PalkaCollision(CharacterController characterController)
        {
            _characterController = characterController;
        }


        public void HandleCollision(ControllerColliderHit hit)
        {
            if (hit.rigidbody!=null&&hit.rigidbody.velocity!=Vector3.zero)
            {
                if (hit.gameObject.TryGetComponent(out IThrowable throwable))
                {
                    Hit?.Invoke();
                } 
            }
        }
        
    }
}