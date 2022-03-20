using UnityEngine;

public interface IThrowable
{
    void ThrowMe(float force, Vector3 direction);
    void TakeMe(Transform placeHolder);
}