using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PacmanGame
{
    [RequireComponent(typeof(Animator))]
    public class ArrowController : MonoBehaviour
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

        void OnGameStart()
        {
            PlayerController player = Globals.Instance.Player;
            player.OnDirectionChange += OnDirectionChange;
        }

        void OnGameFinished()
        {
            PlayerController player = Globals.Instance.Player;
            player.OnDirectionChange -= OnDirectionChange;

        }

        void OnDirectionChange(Vector2Int direction)
        {
            _animator.SetFloat("MoveX", direction.x);
            _animator.SetFloat("MoveY", direction.y);
        }

        private Animator _animator;
    }
}
