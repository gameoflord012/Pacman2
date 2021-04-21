using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAI;

namespace PacmanGame
{
    [RequireComponent(typeof(Animator))]
    public class EnemyController : MonoBehaviour
    {
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
                IPathFinding.Instruction intruction = _pathFindingStrategy.GetInstruction(_moveStrategy.CurrentPosition, Globals.Instance.PlayerRawPosition);
                if (!_isMoving)
                {
                    _isMoving = true;
                    switch (intruction)
                    {
                        case IPathFinding.Instruction.MoveLeft:
                            _moveStrategy.MoveLeft();
                            break;
                        case IPathFinding.Instruction.MoveRight:
                            _moveStrategy.MoveRight();
                            break;
                        case IPathFinding.Instruction.MoveUp:
                            _moveStrategy.MoveUp();
                            break;
                        case IPathFinding.Instruction.MoveDown:
                            _moveStrategy.MoveDown();
                            break;
                    }
                }
            }
        }

        void OnMovementFinished()
        {
            _isMoving = false;
        }

        private void OnGameStart()
        {
            Globals globals = Globals.Instance;
            MapRaw map = globals.CurrentMapRaw;

            // Initialize field
            _isMoving = false;

            // Set strategy pattern
            _moveStrategy = gameObject.AddComponent<EnemyMovement>().Init(transform, map, map.GhostPos);
            _moveStrategy.OnMovementFinshed += OnMovementFinished;

            _pathFindingStrategy = new EnemyPathFinding(map);

            // Spawn entities
            transform.position = globals.CurrentTilemap.GetCellCenterLocal(new Vector3Int(
                map.GhostPos.y,
                map.Size.x - 1 - map.GhostPos.x,
                0
            ));
        }

        private void OnGameFinished()
        {
            _moveStrategy.OnMovementFinshed -= OnMovementFinished;
        }

        private IMoveBehaviour _moveStrategy;
        private IPathFinding _pathFindingStrategy;
        private bool _isMoving;
        private Animator _animator;        
    }
}
