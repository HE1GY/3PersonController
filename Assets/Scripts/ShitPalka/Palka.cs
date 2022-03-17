using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ShitPalka
{
    public class Palka : MonoBehaviour
    {
        [Header("IK")]
        [SerializeField] private Transform _hipRigTarget;
        [SerializeField] private Transform _hipPivot;
        
        [SerializeField] private Transform _leftLegRigTarget;
        [SerializeField] private Transform _leftPivot;
        
        [SerializeField] private Transform _rightLegRigTarget;
        [SerializeField] private Transform _rightPivot;
        
        
        private PalkaIK _palkaIK;

        private void Awake()
        {
            _palkaIK = new PalkaIK(_hipRigTarget, _hipPivot,_leftLegRigTarget,_leftPivot,_rightLegRigTarget,_rightPivot);
        }

        private void Update()
        {
            /*_palkaIK.HandleHipStabilizatin();
            _palkaIK.HandleLeftLegStabilization();
            _palkaIK.HandleRightLegStabilization();*/
        }
    }
}
