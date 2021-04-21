using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacmanGame
{
    public abstract class IMoveBehaviour : MonoBehaviour
    {
        public event Action OnMovementFinshed;
        protected void MovementFinshed()
        {
            OnMovementFinshed?.Invoke();
        }
        public abstract bool MoveRight();
        public abstract bool MoveLeft();
        public abstract bool MoveUp();
        public abstract bool MoveDown();

        protected MapRaw Map; // Mark this as readonly
        protected Vector2Int _currentPos;        

        public Vector2Int CurrentPosition
        {
            get { return _currentPos; }
            private set { }
        }
    }
}