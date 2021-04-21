using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PacmanGame;

namespace PacmanGame
{

    public class PacmanMovement : IMoveBehaviour
    {        
        public PacmanMovement Init(Transform transform, MapRaw map, Vector2Int pos)
        {
            _transform = transform;
            Map = map;
            _currentPos = pos;
            return this;
        }
        public override bool MoveRight() { return MoveX(1); }
        public override bool MoveLeft() { return MoveX(-1); }
        public override bool MoveUp() { return MoveY(1); }
        public override bool MoveDown() { return MoveY(-1); }

        private bool HitWall(Vector2Int pos)
        {
            return Map.IsWall(pos.x, pos.y);
        }

        private bool MoveX(int d)
        {
            Vector2Int nextRawPos = _currentPos + new Vector2Int(0, 1) * d;
            if (HitWall(nextRawPos))
            {
                MovementFinshed();
                return false;
            }
            else
            {
                _currentPos = nextRawPos;
                Vector3 nextPos = _transform.position + Vector3.right * (float)d;
                StartCoroutine(Move(nextPos, MoveTime));
                return true;
            }
        }

        private bool MoveY(int d)
        {
            Vector2Int nextRawPos = _currentPos - new Vector2Int(1, 0) * d;
            if (HitWall(nextRawPos))
            {
                MovementFinshed();
                return false;
            }
            else
            {
                _currentPos = nextRawPos;
                Vector3 nextPos = _transform.position + Vector3.up * (float)d;
                StartCoroutine(Move(nextPos, MoveTime));
                return true;
            }
        }

        IEnumerator Move(Vector3 newPos, float time)
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
                    _transform.position = Vector3.MoveTowards(_transform.position, newPos, delt);
                    lag -= lagCoefficient;
                }

                lastTime = currentTime;
                totalTime += elapsed;

                yield return null;
            }

            MovementFinshed();
            yield return null;
        }

        readonly private float MoveTime = Globals.PlayerMoveTime;
        private Transform _transform;
    }
}