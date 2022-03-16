using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

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
        [SerializeField] private float _takeDistance;

        [Header("IK")]
        [SerializeField] private Transform _armTarget;
    
        private Animator _animator;
        private CharacterController _characterController;
        private RigBuilder _rigBuilder;
    
        private PlayerInput _playerInput;
        private PlayerAnimation _playerAnimation;
        private PlayerMovement _playerMovement;
        private TakeThrower _takeThrower;
        private PlayerIK _playerIK;

        private bool _isInterctable=true;
        


        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _rigBuilder = GetComponent<RigBuilder>();

            MoveValueSetup moveValueSetup = new MoveValueSetup(_walkSpeed, _runSpeed, _jumpHeight);
        
            _playerInput = new PlayerInput();
            _playerMovement = new PlayerMovement(_characterController,_playerInput,moveValueSetup,_camTransform,_groundMask);
            _playerAnimation = new PlayerAnimation(_animator);
            _takeThrower = new TakeThrower(_placeHolder, _camTransform, _itemMask, _playerInput,_throwForce,_takeDistance);
            _playerIK = new PlayerIK(_rigBuilder, _armTarget);
            
            
            _playerMovement.Grounded += _playerAnimation.SetLandedTrigger;
            _takeThrower.Take += OnTaking;
            _takeThrower.Throw += OnThrowing;
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

        public void SubscribeOnCanTake(Action<bool> method)
        {
            _takeThrower.CanTake += method;
        }

        public void UnSubscribeOnCanTake(Action<bool> method)
        {
            _takeThrower.CanTake -= method;
        }

        private void OnTaking(Vector3 targetPos)
        {
            _playerAnimation.PlayTaking();
            _playerIK.SetActiveArmRig();
            _playerIK.TakingIKAnimation(targetPos);
        }

        private void OnThrowing()
        {
            _playerAnimation.PlayThrowing();
        }
        

        // Animation Methods
        private void ToMakeStepsInWalkRunAnimation()
        {
            MakeStep?.Invoke();
        }

        private void SetInteractableFalse()
        {
            _isInterctable = false;
        }

        private void SetInteractableTrue()
        {
            _isInterctable = true;
        }

        private void InTakeAnimation()
        {
            _takeThrower.FinallyTake();
            _playerIK.SetUnActiveArmRig();
        }

        private void InThrowAnimation()
        {
            _takeThrower.FinallyThrow();
        }

        private void InTheEndThrowAnimation()
        {
            _playerAnimation.BackFromThrowing();
        }
    }
}