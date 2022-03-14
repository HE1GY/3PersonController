using System;
using UnityEngine;

namespace PlayerScripts
{
    public class TakeThrower
    {
        public event Action<bool> CanTake;
        
        private readonly Transform _placeHolder;
        private readonly Transform _camTransform;
        private readonly LayerMask _mask;
        private readonly PlayerInput _input;
        private readonly float _throwForce;

        private Item _item;
        
        public TakeThrower(Transform placeHolder,Transform camTransform,LayerMask mask,PlayerInput input,float throwForce)
        {
            _placeHolder = placeHolder;
            _camTransform = camTransform;
            _mask = mask;
            _input = input;
            _throwForce = throwForce;

            _input.Player.TakeItem.performed += _ =>
            {
                TakingItem();
            };

            _input.Player.ThrowItem.performed += _ =>
            {
                _item?.ThrowMe(_throwForce,_camTransform.forward);
            };

            _input.Player.CameraRotation.performed += _ =>
            {
                CanTake?.Invoke(TryTake(out Item item));
            };

        }

        private bool TryTake(out Item item)
        {
            Ray ray;
            Vector3 rayOrigin=_camTransform.position;
            Vector3 rayDirection = _camTransform.forward;
            ray=new Ray(rayOrigin,rayDirection);
            
            Debug.DrawRay(rayOrigin,rayDirection,Color.red,100);
            
            if (Physics.Raycast(ray,out RaycastHit hit,100,_mask))
            {
                if (hit.collider.gameObject.TryGetComponent<Item>(out Item item1))
                {
                    item = item1;
                    return true;
                } 
                
            }
            item = null;
            return false;
        }

        private void TakingItem()
        {
            if (TryTake(out Item item))
            {
                item.TakeMe(_placeHolder);
                _item = item;
            }
        }
        
    }
}
