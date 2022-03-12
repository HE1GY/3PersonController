using UnityEngine;

public class PlayerAnimation
{
    private static readonly int _velocityHash = Animator.StringToHash("velocity");
    
    private Animator _animator;

    public PlayerAnimation(Animator animator)
    {
        _animator = animator;
    }

    public void SetVelocity(float velocity)
    {
        _animator.SetFloat(_velocityHash,velocity);
    }
}
