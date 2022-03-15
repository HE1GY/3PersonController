using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : StateMachineBehaviour
{
    private  readonly int _landedHash = Animator.StringToHash("landed");

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(_landedHash);
    }
}
