using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("MoveSetup")]
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private Transform _camTransform;
    
    [Header("Gravity")]
    [SerializeField] private LayerMask _groundMask;
    
    private Animator _animator;
    private CharacterController _characterController;
    
    private PlayerInput _playerInput;
    private PlayerAnimation _playerAnimation;
    private PlayerMovement _playerMovement;
    

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        MoveValueSetup moveValueSetup = new MoveValueSetup(_walkSpeed, _runSpeed, _jumpHeight);
        
        _playerInput = new PlayerInput();
        _playerMovement = new PlayerMovement(_characterController,_playerInput,moveValueSetup,_camTransform,_groundMask);
        _playerAnimation = new PlayerAnimation(_animator);
        
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
        _playerMovement.HandleHorizontalMove();
        _playerMovement.HandleRotation();
        _playerMovement.HandleVerticalMove();
        _playerAnimation.SetHorizontalVelocity(_playerMovement.GetNormalizedHorizontalVelocity());
        _playerAnimation.SetVerticallVelocity(_playerMovement.GetVerticalVelocity());
    }
}