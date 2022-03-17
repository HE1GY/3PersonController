using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace PlayerScripts
{
    public class PlayerIK
    {
        private const string _armRigName="ArmRig";

        private RigBuilder _rigBuilder;
        private Transform _armTarget;
        private RigLayer _armRig;


        public PlayerIK(RigBuilder rigBuilder, Transform armTarget)
        {
            _rigBuilder = rigBuilder;
            _armTarget = armTarget;
            FindArmRig();
        }

        public void SetActiveArmRig()
        {
            _armRig.active = true;
        }

        public void SetUnActiveArmRig()
        {
            _armRig.active = false;
        }
        public void OnTakingIKAnimation(Vector3 targetPosition)
        {
            _armTarget.position = targetPosition;
        }

        private void FindArmRig()
        {
            _armRig= _rigBuilder.layers.First(r => r.name == _armRigName);
        }
    }
}
