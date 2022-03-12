using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement
{
    private const float Acceleration = 0.05f;
    private const float Deceleration = 0.05f;
    private const float TurnSpeed =10;
    private const float Gravity =9.8f;
    
    private readonly CharacterController _characterController;
    private readonly PlayerInput _input;
    private readonly Transform _camTransform;

    private float _runSpeed;
    private float _walkSpeed;
    private float _currentSpeed;
    private float _turnSmothVelocity;
    private float _gravityVelocity=0;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _moveDirection;

    private bool _isRunPress;
    private bool  _isWalkPress;

    public PlayerMovement(CharacterController characterController, PlayerInput input,float walkSpeed,float runSpeed, Transform camTransform)
    {
        _characterController= characterController;
        _input = input;
        _walkSpeed = walkSpeed;
        _runSpeed = runSpeed;
        _camTransform = camTransform;

        _input.Player.Walk.performed += OnInputDirection;
        _input.Player.Walk.canceled += _=>_isWalkPress=false ;

        _input.Player.Run.started += OnInputRun;
        _input.Player.Run.canceled += OnInputRun;
    }

    
    private void OnInputDirection(InputAction.CallbackContext ctx)
    {
        Vector2 inputVector = ctx.ReadValue<Vector2>();
        _isWalkPress = inputVector.x!=0||inputVector.y!=0;
        _horizontalInput = inputVector.x;
        _verticalInput = inputVector.y;
    }

    
    private void OnInputRun(InputAction.CallbackContext ctx) => _isRunPress = ctx.ReadValueAsButton();


    public void HandleMove()
    {
        _moveDirection = GetMoveDirection();
        
        if (_isWalkPress && _currentSpeed <=_walkSpeed)
        {
            _currentSpeed += Acceleration;
            _currentSpeed=RoundValue(_currentSpeed, _walkSpeed, true);
        }
        else if(!_isWalkPress&&_currentSpeed>0)
        {
            _currentSpeed -= Deceleration;
            _currentSpeed=RoundValue(_currentSpeed, 0, false);
        }
        
        if (_isRunPress &&_isWalkPress&& _currentSpeed <=_runSpeed&&_currentSpeed>=_walkSpeed)
        {
            _currentSpeed += Acceleration;
            _currentSpeed=RoundValue(_currentSpeed, _runSpeed, true);
        }
        else if(!_isRunPress&& _currentSpeed>_walkSpeed)
        {
            _currentSpeed -= Deceleration;
            _currentSpeed = RoundValue(_currentSpeed, _walkSpeed, false);
        }
        _characterController.Move(_moveDirection * _currentSpeed * Time.deltaTime);
    }

    public void HandleRotation()
    {
        if (_isWalkPress)
        {
            Vector3 targetDiraction = GetMoveDirection();
            Quaternion targetRotation = Quaternion.LookRotation(targetDiraction);
            Quaternion newRotation=Quaternion.Slerp(_characterController.transform.rotation,targetRotation,TurnSpeed*Time.deltaTime);
            _characterController.transform.rotation = newRotation;
        }
    }

    public void HandleGravity()
    {
        /*if (Physics.CheckSphere(_characterController.transform.position, 0.4f,6))
        {
            Debug.Log("Land");
        }
        else
        {
            Debug.Log("Falling");
        }*/
        
        
        /*_characterController.Move(-_gravityVelocity * Vector3.up * Time.deltaTime);*/
    }
    
    
    
    public float GetNormalizedVelocity() => _currentSpeed / _runSpeed;


    private Vector3 GetMoveDirection()
    {
        Vector3 direction = _verticalInput * _camTransform.forward;
        direction += _camTransform.right * _horizontalInput;
        direction.Normalize();
        direction.y = 0;
        return direction;
    }
    private float RoundValue(float value, float toValue,bool isLess)
    {
        float error=0.05f;
        if (isLess&&toValue-error<value)
        {
            value = toValue;
        }
        else if (!isLess && toValue + error > value)
        {
            value = toValue;
        }
        return value;
    }
}