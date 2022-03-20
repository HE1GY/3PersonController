using System;
using System.Security.Cryptography;
using ShitPalka;
using UnityEngine;

namespace PlayerScripts
{
    public class TakeThrower
    {
        public event Action<bool> CanTake;
        public event Action<Vector3> TakeItem;
        public event Action ThrowItem;
        
        private readonly Transform _placeHolder;
        private readonly Transform _camTransform;
        private readonly LayerMask _mask;
        private readonly float _throwForce;
        private readonly float _takeDistance;

        private IThrowable _IThrowableItem;

        public TakeThrower(Transform placeHolder,Transform camTransform,LayerMask mask,PlayerInput input,float throwForce,float takeDistance)
        {
            _placeHolder = placeHolder;
            _camTransform = camTransform;
            _mask = mask;
            PlayerInput input1 = input;
            _throwForce = throwForce;
            _takeDistance = takeDistance;


            input1.Player.TakeItem.performed += _ =>
            {
                TakingItem();
            };

            input1.Player.ThrowItem.performed += _ =>
            {
                if (_IThrowableItem != null)
                {
                    ThrowItem?.Invoke();
                }
            };

            input1.Player.CameraRotation.performed += _ =>
            {
                bool canTake = TryTake(out Item item);
                CanTake?.Invoke(canTake);
            };

        }

        #region Animation Methods

        public void FinallyTake()
        {
            _IThrowableItem.TakeMe(_placeHolder);
        }

        public void FinallyThrow()
        {
            _IThrowableItem.ThrowMe(_throwForce,_camTransform.forward);
            _IThrowableItem = null;
        }
        #endregion
        

        private bool TryTake(out Item item)
        {
            Ray ray = new Ray(_camTransform.position, _camTransform.forward);
            if (Physics.Raycast(ray,out RaycastHit raycastHit,100,_mask))
            {
                float distanceToItem = Vector3.Distance(_placeHolder.position, raycastHit.point);
                if (distanceToItem <= _takeDistance)
                {
                    if (raycastHit.collider.gameObject.TryGetComponent<Item>(out Item item1))
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
                if (_IThrowableItem == null)
                {
                    _IThrowableItem = item;
                    TakeItem?.Invoke(item.transform.position);
                }
            }
        }
    }
}
