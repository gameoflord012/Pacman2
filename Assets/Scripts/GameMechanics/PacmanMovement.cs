using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour, IMoveBehaviour
{
    readonly float MoveTime = Globals.PlayerMoveTime;
    public event Action OnMovementFinshed;

    event Action IMoveBehaviour.OnMovementFinshed
    {
        add
        {
            OnMovementFinshed += value;
        }

        remove
        {
            OnMovementFinshed -= value;
        }
    }

    public void MoveX(Transform t, int d)
    {
        Globals global = Globals.Instance;        
        Vector2Int nextRawPos = global.CurrentRawPosition + new Vector2Int(0, 1) * d;
        if (HitWall(nextRawPos))
        {
            OnMovementFinshed();            
        }
        else
        {
            global.CurrentRawPosition = nextRawPos;
            Vector3 nextPos = t.position + Vector3.right * (float)d;
            StartCoroutine(Move(t, nextPos, MoveTime));
        }
    }

    public void MoveY(Transform t, int d)
    {
        Globals global = Globals.Instance;
        Vector2Int nextRawPos = global.CurrentRawPosition - new Vector2Int(1, 0) * d;
        if (HitWall(nextRawPos))
        {
            OnMovementFinshed();            
        }
        else
        {
            global.CurrentRawPosition = nextRawPos;
            Vector3 nextPos = t.position + Vector3.up * (float)d;
            StartCoroutine(Move(t, nextPos, MoveTime));
        }
    }

    private bool HitWall(Vector2Int pos)
    {
        MapRaw map = Globals.Instance.CurrentMapRaw;
        return map.GetTile(pos.x, pos.y) == Globals.CellFormat.Wall;
    }

    IEnumerator Move(Transform ti, Vector3 newPos, float time)
    {
        Debug.Log(Globals.Instance.CurrentRawPosition);

        float lastTime = Time.time;
        float totalTime = 0;

        float lagCoefficient = 1f / 60f;
        float lag = 0;
        float delt = lagCoefficient / time;

        while (totalTime <= time)
        {
            float currentTime = Time.time;
            float elapsed = currentTime - lastTime;
            lag += elapsed;

            while (lag >= lagCoefficient)
            {
                ti.position = Vector3.MoveTowards(ti.position, newPos, delt);
                lag -= lagCoefficient;
            }

            lastTime = currentTime;
            totalTime += elapsed;

            yield return null;
        }

        OnMovementFinshed();
        yield return null;
    }
}
