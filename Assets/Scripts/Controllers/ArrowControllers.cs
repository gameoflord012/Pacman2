using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ArrowControllers : MonoBehaviour
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
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
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }
}
