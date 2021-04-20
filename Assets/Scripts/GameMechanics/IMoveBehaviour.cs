using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IMoveBehaviour
{    
    event Action OnMovementFinshed;

    bool MoveRight(Transform t);
    bool MoveLeft(Transform t);
    bool MoveUp(Transform t);
    bool MoveDown(Transform t);
}
