using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace ShitPalka
{
    public class PalkaIK
    {
        private const float _stepOffSet=0;
        
        private readonly Transform _hipRigTarget;
        private readonly Transform _hipPivot;
        
        private readonly Transform _leftLegRigTarget;
        private readonly Transform _leftPivot;

        private readonly Transform _rightLegRigTarget;
        private readonly Transform _rightPivot;

        public PalkaIK( Transform hipRigTarget,Transform hipPivot, Transform leftLegRigTarget, Transform leftPivot, Transform rightLegRigTarget, Transform rightPivot)
        {
            _hipRigTarget = hipRigTarget;
            _hipPivot = hipPivot;
            _leftLegRigTarget = leftLegRigTarget;
            _leftPivot = leftPivot;
            _rightLegRigTarget = rightLegRigTarget;
            _rightPivot = rightPivot;
        }

        public void HandleHipStabilizatin()
        {
            Ray ray;
            Vector3 rayOrigin=_hipPivot.position;
            Vector3 rayDirection=-_hipPivot.up;
            ray = new Ray(rayOrigin, rayDirection);
            
            

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000,LayerMask.GetMask("Ground")))
            {
                Debug.DrawRay(rayOrigin,rayDirection,Color.cyan,100);
                Vector3 surfaceNormale = hitInfo.normal;
                _hipRigTarget.up = surfaceNormale;
            }
        }

        public void HandleLeftLegStabilization()
        {
            Ray ray;
            Vector3 rayOrigin=_leftPivot.position;
            Vector3 rayDirection=Vector3.down;
            ray = new Ray(rayOrigin, rayDirection);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100,LayerMask.GetMask("Ground")))
            {
                Vector3 hitPos = hitInfo.point;
                if (Vector3.Distance(_leftLegRigTarget.position, hitPos) > 0.3f)
                {
                    _leftLegRigTarget.position = hitPos;
                }
            }
        }
        public void HandleRightLegStabilization()
        {
            Ray ray;
            Vector3 rayOrigin=_rightPivot.position;
            Vector3 rayDirection=Vector3.down;
            ray = new Ray(rayOrigin, rayDirection);
            
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100,LayerMask.GetMask("Ground")))
            {
                Vector3 hitPos = hitInfo.point;
                if (Vector3.Distance(_rightLegRigTarget.position, hitPos) > 0.2f)
                {
                    _rightLegRigTarget.position = hitPos;
                }
            }
        }
        
        
    }
}
