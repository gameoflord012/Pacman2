using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private void Awake()
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

    void Update()
    {
        Vector2Int inputNext = GetInput();
        if (inputNext.magnitude > 0.5f)
        {
            inputHolder = inputNext;
            OnDirectionChange(inputHolder);
        }

        if (!isMoving)
        {
            if(MovePlayer(inputHolder))
            {
                currentDirection = inputHolder;
            }
            else
            {
                MovePlayer(currentDirection);
            }                
        }
    }

    Vector2Int GetInput()
    {
        return new Vector2Int(
            Math.Sign(Input.GetAxisRaw("Horizontal")),
            Math.Sign(Input.GetAxisRaw("Vertical"))
            );
    }
    bool MovePlayer(Vector2Int direction)
    {
        isMoving = true;
        if (direction == Vector2Int.left)
        {
            animator.SetFloat("MoveX", -1);
            animator.SetFloat("MoveY", 0);
            return moveStrategy.MoveLeft(transform);
        }
        else if (direction == Vector2Int.right)
        {
            animator.SetFloat("MoveX", 1);
            animator.SetFloat("MoveY", 0);
            return moveStrategy.MoveRight(transform);
        }
        else if (direction == Vector2Int.up)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", 1);
            return moveStrategy.MoveUp(transform);
        }
        else if (direction == Vector2Int.down)
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", -1);
            return moveStrategy.MoveDown(transform);
        }
        else if (direction == Vector2Int.zero)
        {
            isMoving = false;
            return true;
        }        
        throw new InvalidPlayerDirection("Invalid Vector2Int value");
    }

    void OnMovementFinished()
    {
        isMoving = false;
    }

    void OnGameStart()
    {        
        Globals globals = Globals.Instance;
        MapRaw map = globals.CurrentMapRaw;

        isMoving = false;
        inputHolder = Vector2Int.right;
        currentDirection = Vector2Int.right;

        moveStrategy = gameObject.AddComponent<PacmanMovement>().Init(map, map.PlayerPos);
        moveStrategy.OnMovementFinshed += OnMovementFinished;

        transform.position = globals.CurrentTilemap.GetCellCenterLocal(new Vector3Int(
            map.PlayerPos.y,
            map.Size.x - 1 - map.PlayerPos.x, 
            0
        ));
    }

    void OnGameFinished()
    {
        moveStrategy.OnMovementFinshed -= OnMovementFinished;
    }

    public delegate void DirectionHandler(Vector2Int direction);
    public event DirectionHandler OnDirectionChange;

    IMoveBehaviour moveStrategy;
    bool isMoving = false;
    Animator animator;
    Vector2Int inputHolder;
    Vector2Int currentDirection;    
}
