using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{        
    private void Awake()
    {        
        moveStrategy = gameObject.AddComponent<PacmanMovement>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        moveStrategy.OnMovementFinshed += OnMovementFinished;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
        moveStrategy.OnMovementFinshed -= OnMovementFinished;
    }

    void Update()
    {
        Vector2 inputNext = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (inputNext.magnitude > 0.5f)
            inputHolder = inputNext;

        if (!isMoving)
        {            
            if (inputHolder.x < -0.5f)
            {
                isMoving = true;
                moveStrategy.MoveX(transform, -1);
                animator.SetFloat("MoveX", -1);
                animator.SetFloat("MoveY", 0);
            }
            else if (inputHolder.x > 0.5f)
            {
                isMoving = true;
                moveStrategy.MoveX(transform, 1);
                animator.SetFloat("MoveX", 1);
                animator.SetFloat("MoveY", 0);
            }
            else if(inputHolder.y < -0.5f)
            {
                isMoving = true;
                moveStrategy.MoveY(transform, -1);
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", -1);
            }
            else if (inputHolder.y > 0.5f)
            {
                isMoving = true;
                moveStrategy.MoveY(transform, 1);
                animator.SetFloat("MoveX", 0);
                animator.SetFloat("MoveY", 1);
            }
        }
    }

    void OnMovementFinished()
    {
        isMoving = false;
    }

    void OnGameStart()
    {
        isMoving = false;
        inputHolder = Vector2.zero;
        Globals globals = Globals.Instance;
        MapRaw map = globals.CurrentMapRaw;
        transform.position = globals.CurrentTilemap.GetCellCenterLocal(new Vector3Int(
            map.PlayerPos.y,
            map.Size.x - 1 - map.PlayerPos.x, 
            0
         ));
    }    

    IMoveBehaviour moveStrategy;
    private bool isMoving = false;
    private Animator animator;
    Vector2 inputHolder;
}
