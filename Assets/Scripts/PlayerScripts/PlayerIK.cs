using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace PlayerScripts
{
    public class PlayerIK
    {
        private Transform _headTarget;
        private Transform _rArmTarget;
        
        private Rig _rArmRig;
        private Rig _HeadRig;


        public PlayerIK(Rig rArmRig)
        {
            _rArmRig = rArmRig;

            _rArmTarget = _rArmRig.GetComponentInChildren<TwoBoneIKConstraint>().data.target;
        }

        public void SetActiveArmRig()
        {
            _rArmRig.weight = 1;
        }

        public void SetUnActiveArmRig()
        {
            _rArmRig.weight = 0;
        }
        public void OnTakingIKAnimation(Vector3 targetPosition)
        {
            _rArmTarget.position = targetPosition;
        }
        
    }
}
