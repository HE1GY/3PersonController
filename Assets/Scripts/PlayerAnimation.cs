using UnityEngine;

public class PlayerAnimation
{
    private static readonly int _velocityHorizontalHash = Animator.StringToHash("horizontalVelocity");
    private static readonly int _verticalVelocityHash = Animator.StringToHash("verticalVelocity");
    private static readonly int _landedHash = Animator.StringToHash("landed");

    private Animator _animator;

    public PlayerAnimation(Animator animator)
    {
        _animator = animator;
    }

    public void SetHorizontalVelocity(float velocity)
    {
        _animator.SetFloat(_velocityHorizontalHash,velocity);
    }
    public void SetVerticallVelocity(float velocity)
    {
        _animator.SetFloat(_verticalVelocityHash,velocity);
    }

    public void SetLandedTrigger()
    {
        _animator.SetTrigger(_landedHash);
    }
    
}
