using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameAI
{    
    public abstract class IPathFinding
    {
        public abstract Instruction GetInstruction(Vector2Int currentPos, Vector2Int targetPos);

        public enum Instruction
        {
            MoveLeft,
            MoveRight,
            MoveUp,
            MoveDown
        }
    }
}