﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control
{
  public class PlayerController : MonoBehaviour
  {
    void Update()
    {
      if (Input.GetMouseButton(0))
      {
        MoveToMousePosition();
      }
    }

    private void MoveToMousePosition()
    {
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); ;
      RaycastHit hit;
      bool hasHit = Physics.Raycast(ray, out hit);

      if (hasHit)
      {
        GetComponent<Mover>().MoveTo(hit.point);
      }
    }
  }

}