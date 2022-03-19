using UnityEngine;

namespace ShitPalka
{
    public class PalkaMover
    {
        private CharacterController _characterController;
        private float _speed;
        private Transform _targetToMove;

        public PalkaMover(CharacterController characterController, float speed, Transform targetToMove)
        {
            _characterController = characterController;
            _speed = speed;
            _targetToMove = targetToMove;
        }


        public void HandleMovement()
        {
            _characterController.SimpleMove(_characterController.transform.forward * _speed);
        }


        public void HandleRotation()
        {
            
        }
    }
}