using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacmanGame
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public delegate void DirectionHandler(Vector2Int direction);
        public event DirectionHandler OnDirectionChange;
        public event DirectionHandler OnPlayerRawPositionChange;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameFinished += OnGameFinished;
        }

        private void OnDisable()
        {
            GameManager.OnGameStart -= OnGameStart;
            GameManager.OnGameFinished -= OnGameFinished;
        }

        private void Update()
        {
            if (GameManager.CurrentGameState == GameManager.GameState.GameStart)
            {
                Vector2Int inputNext = GetInput();
                if (inputNext.magnitude > 0.5f)
                {
                    _inputHolder = inputNext;
                    OnDirectionChange?.Invoke(_inputHolder);
                }

                if (!_isMoving)
                {
                    if (MovePlayer(_inputHolder))
                    {
                        _currentDirection = _inputHolder;
                    }
                    else
                    {
                        MovePlayer(_currentDirection);
                    }
                }
            }
        }

        Vector2Int GetInput()
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Mathf.Abs(input.x) > 0.5f) return new Vector2Int(Math.Sign(input.x), 0);
            if (Mathf.Abs(input.y) > 0.5f) return new Vector2Int(0, Math.Sign(input.y));
            return Vector2Int.zero;
        }
        bool MovePlayer(Vector2Int direction)
        {
            _isMoving = true;
            bool result = false;
            if (direction == Vector2Int.left)
            {
                _animator.SetFloat("MoveX", -1);
                _animator.SetFloat("MoveY", 0);
                result = _moveStrategy.MoveLeft();
            }
            else if (direction == Vector2Int.right)
            {
                _animator.SetFloat("MoveX", 1);
                _animator.SetFloat("MoveY", 0);
                result = _moveStrategy.MoveRight();
            }
            else if (direction == Vector2Int.up)
            {
                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", 1);
                result = _moveStrategy.MoveUp();
            }
            else if (direction == Vector2Int.down)
            {
                _animator.SetFloat("MoveX", 0);
                _animator.SetFloat("MoveY", -1);
                result = _moveStrategy.MoveDown();
            }
            else if (direction == Vector2Int.zero)
            {
                _isMoving = false;
                result = true;
            }
            OnPlayerRawPositionChange(_moveStrategy.CurrentPosition);
            return result;
        }

        void OnMovementFinished()
        {
            _isMoving = false;
        }

        void OnGameStart()
        {
            Globals globals = Globals.Instance;
            MapRaw map = globals.CurrentMapRaw;

            // Initalize field
            _isMoving = false;
            _inputHolder = Vector2Int.right;
            _currentDirection = Vector2Int.right;
            OnDirectionChange?.Invoke(_inputHolder);

            // Set strategy pattern
            _moveStrategy = gameObject.AddComponent<PacmanMovement>().Init(transform, map, map.PlayerPos);
            _moveStrategy.OnMovementFinshed += OnMovementFinished;

            // Spawn entities
            transform.position = globals.CurrentTilemap.GetCellCenterLocal(new Vector3Int(
                map.PlayerPos.y,
                map.Size.x - 1 - map.PlayerPos.x,
                0
            ));
        }

        void OnGameFinished()
        {
            _moveStrategy.OnMovementFinshed -= OnMovementFinished;
        }

        IMoveBehaviour _moveStrategy;
        public IMoveBehaviour MoveStrategy
        {
            get
            {
                return _moveStrategy;
            }
            private set { }
        }

        bool _isMoving;
        private Animator _animator;
        private Vector2Int _inputHolder;
        private Vector2Int _currentDirection;
    }
}