using System;
using UnityEngine;

namespace ShitPalka
{
    public  class RayThrower
    {
        private Transform _rayOrg;
        private Vector3 _rayDir = Vector3.down;

        public RayThrower(Transform rayOrg)
        {
            _rayOrg = rayOrg;
        }

        public Vector3 GetHitPos()
        {
            Ray ray;
            ray = new Ray(_rayOrg.position, _rayDir);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100, LayerMask.GetMask("Ground")))
            {
                return hitInfo.point;
            }
            
            Debug.LogError("Leg RayCast Not hit ground");
            throw new Exception("Leg RayCast Not hit ground");
        }
    
    }
}