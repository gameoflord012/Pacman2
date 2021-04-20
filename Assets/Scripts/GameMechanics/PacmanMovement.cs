using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanMovement : MonoBehaviour, IMoveBehaviour
{
    public PacmanMovement Init(MapRaw map, Vector2Int pos)
    {
        Map = map;
        CurrentPos = pos;
        return this;
    }        
    public bool MoveRight(Transform t) { return MoveX(t, 1); }
    public bool MoveLeft(Transform t) { return MoveX(t, -1); }
    public bool MoveUp(Transform t) { return MoveY(t, 1); }
    public bool MoveDown(Transform t) { return MoveY(t, -1); }

    private bool HitWall(Vector2Int pos)
    {
        return Map.GetTile(pos.x, pos.y) == Globals.CellFormat.Wall;
    }

    private bool MoveX(Transform t, int d)
    {        
        Vector2Int nextRawPos = CurrentPos + new Vector2Int(0, 1) * d;
        if (HitWall(nextRawPos))
        {
            OnMovementFinshed();
            return false;
        }
        else
        {
            CurrentPos = nextRawPos;
            Vector3 nextPos = t.position + Vector3.right * (float)d;
            StartCoroutine(Move(t, nextPos, MoveTime));
            return true;
        }
    }

    private bool MoveY(Transform t, int d)
    {        
        Vector2Int nextRawPos = CurrentPos - new Vector2Int(1, 0) * d;
        if (HitWall(nextRawPos))
        {
            OnMovementFinshed();
            return false;
        }
        else
        {
            CurrentPos = nextRawPos;
            Vector3 nextPos = t.position + Vector3.up * (float)d;
            StartCoroutine(Move(t, nextPos, MoveTime));
            return true;
        }
    }

    IEnumerator Move(Transform ti, Vector3 newPos, float time)
    {        
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

    readonly float MoveTime = Globals.PlayerMoveTime;
    public MapRaw Map;
    public Vector2Int CurrentPos;

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

}
