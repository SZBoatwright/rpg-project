﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
  [SerializeField] Transform target;

  void LateUpdate()
  {
    gameObject.transform.position = target.position;
  }
}