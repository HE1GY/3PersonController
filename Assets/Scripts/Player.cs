using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _runSpeed;
    [SerializeField] private Transform _camTransform;
    
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
        
        _playerInput = new PlayerInput();
        _playerAnimation = new PlayerAnimation(_animator);
        _playerMovement = new PlayerMovement(_characterController,_playerInput,_walkSpeed,_runSpeed,_camTransform);

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
        _playerMovement.HandleMove();
        _playerMovement.HandleRotation();
        _playerMovement.HandleGravity();
        _playerAnimation.SetVelocity(_playerMovement.GetNormalizedVelocity());
    }
}