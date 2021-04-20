using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMoveBehaviour
{    
    event Action OnMovementFinshed;
    void MoveX(Transform t, int d);
    void MoveY(Transform t, int d);
}
