using System;
using UnityEngine;

namespace PlayerScripts
{
    public class TakeThrower
    {
        public event Action<bool> CanTake;
        public event Action<Vector3> Take;
        public event Action Throw;
        
        private readonly Transform _placeHolder;
        private readonly Transform _camTransform;
        private readonly LayerMask _mask;
        private readonly PlayerInput _input;
        private readonly float _throwForce;
        private readonly float _takeDistance;

        private Item _item;

        public TakeThrower(Transform placeHolder,Transform camTransform,LayerMask mask,PlayerInput input,float throwForce,float takeDistance)
        {
            _placeHolder = placeHolder;
            _camTransform = camTransform;
            _mask = mask;
            _input = input;
            _throwForce = throwForce;
            _takeDistance = takeDistance;

            _input.Player.TakeItem.performed += _ =>
            {
                TakingItem();
            };

            _input.Player.ThrowItem.performed += _ =>
            {
                if (_item != null)
                {
                    Throw?.Invoke();
                }
            };

            _input.Player.CameraRotation.performed += _ =>
            {
                bool canTake = TryTake(out Item item);                
                CanTake?.Invoke(canTake);
            };

        }

        public void FinallyTake()
        {
            _item.TakeMe(_placeHolder);
        }

        public void FinallyThrow()
        {
            _item.ThrowMe(_throwForce,_camTransform.forward);
            _item = null;
        }
        

        private bool TryTake(out Item item)
        {
            Ray ray;
            Vector3 rayOrigin=_camTransform.position;
            Vector3 rayDirection = _camTransform.forward;
            ray=new Ray(rayOrigin,rayDirection);
            
            if (Physics.Raycast(ray,out RaycastHit hit,100,_mask))
            {
                float distanceToItem = Vector3.Distance(_placeHolder.position, hit.point);
                if (distanceToItem <= _takeDistance)
                {
                    if (hit.collider.gameObject.TryGetComponent<Item>(out Item item1))
                    {
                        item = item1;
                        return true;
                    } 
                }
            }
            item = null;
            return false;
        }

        private void TakingItem()
        {
            if (TryTake(out Item item))
            {
                if (_item == null)
                {
                    _item = item;
                    Take?.Invoke(item.transform.position);
                }
            }
        }
    }
}
