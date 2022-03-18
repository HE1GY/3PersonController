using System;
using System.Security.Cryptography;
using UnityEngine;

namespace ShitPalka
{
    public class Leg
    {

        private const float _distanceToMove = 0.5f;

        private Transform _ikTargetTransform;
        private Vector3 _currentPos;
        private Transform _rayOrg;

        private RayThrower _rayThrower;
        private LegAnimation _legAnimation;

        private float _currentPoint=0;
        private bool _stay=true;
        private Vector3 _targetPos;
        private Vector3 _startPos;


        public Leg(Transform ikTargetTransform,  Transform rayOrg,Animator animator)
        {
            _ikTargetTransform = ikTargetTransform;
            _rayOrg = rayOrg;
            _rayThrower = new RayThrower(_rayOrg);
            _currentPos=_rayThrower.GetHitPos();
            _legAnimation = new LegAnimation(animator);
        }

        private bool CheckIfMoveToPos(out Vector3 nextPos)
        {
            nextPos = _rayThrower.GetHitPos();
            return Vector3.Distance(_ikTargetTransform.position, nextPos) > _distanceToMove;
        }

        public void HandleMovement()
        {
            if (CheckIfMoveToPos(out Vector3 nextPos)&&_stay)
            {
                _legAnimation.PlayLegMove();
                _startPos = _ikTargetTransform.position;
                _targetPos = nextPos;
                _currentPos = nextPos;
                _stay = false;
            }
            else if(_stay)
            {
                _ikTargetTransform.position = _currentPos;
            }
            else
            {
                _ikTargetTransform.position = Vector3.Lerp(_startPos, _targetPos, _currentPoint);
                _currentPoint += Time.deltaTime*3;
                if (_currentPoint >= 1)
                {
                    _currentPoint = 0;
                    _stay = true;
                }
            }
        }
    }
}