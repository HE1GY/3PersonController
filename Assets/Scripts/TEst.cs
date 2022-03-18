using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEst : MonoBehaviour
{
   [SerializeField] private Vector3 _currentPos;
   [SerializeField] private Transform _Ik;

   private void Update()
   {
      _Ik.position = _currentPos;
   }
}
