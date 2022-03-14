using System;
using UnityEngine;

namespace PlayerScripts
{
    public class Player : MonoBehaviour
    {
        public event Action MakeStep;

        [Header("MoveSetup")]
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _runSpeed;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private Transform _camTransform;
    
        [Header("Gravity")]
        [SerializeField] private LayerMask _groundMask;

        [Header("Take&Throw")] 
        [SerializeField] private Transform _placeHolder;
        [SerializeField] private LayerMask _itemMask;
        [SerializeField] private float _throwForce;
    
        private Animator _animator;
        private CharacterController _characterController;
    
        private PlayerInput _playerInput;
        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        private TakeThrower _takeThrower;

        private bool _isInterctable=true;
    

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();

            MoveValueSetup moveValueSetup = new MoveValueSetup(_walkSpeed, _runSpeed, _jumpHeight);
        
            _playerInput = new PlayerInput();
            _playerMovement = new PlayerMovement(_characterController,_playerInput,moveValueSetup,_camTransform,_groundMask);
            _playerAnimation = new PlayerAnimation(_animator);
            _takeThrower = new TakeThrower(_placeHolder, _camTransform, _itemMask, _playerInput,_throwForce);
            
            _playerMovement.Grounded += _playerAnimation.SetLandedTrigger; 
        }
    
        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }

        private void Update()
        {
            _playerMovement.HandleHorizontalMove(_isInterctable);
            _playerMovement.HandleRotation();
            _playerMovement.HandleVerticalMove();
            _playerAnimation.SetHorizontalVelocity(_playerMovement.GetNormalizedHorizontalVelocity());
            _playerAnimation.SetVerticalVelocity(_playerMovement.GetVerticalVelocity());
        }

        public float GetSpeed() => _playerMovement.GetVelocity();

        private void SetInteractableTrue()
        {
            _isInterctable = true;
        }
        private void SetInteractableFalse()
        {
            _isInterctable = false;
        }

        private void ToMakeSteps()
        {
            MakeStep?.Invoke();
        }

        public void SubscribeOnCanTake(Action<bool> method)
        {
            _takeThrower.CanTake += method;
        }
        public void UnSubscribeOnCanTake(Action<bool> method)
        {
            _takeThrower.CanTake -= method;
        }
    
    }
}