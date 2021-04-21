using System;
using UnityEngine;

namespace GameAI
{

    public class EnemyPathFinding : IPathFinding
    {
        /* Start coding here ...
         * map.GetTile(x, y) to retrive informations at tile (x, y)
         * 
         * Example:
         * To check if a tile is wall
         *      Map.IsWall(x, y) == Globals.CellFormat.Wall
         *      
         * To check if a tile is space
         *      Map.IsSpace(x, y) == true
         *      
         * To get world width
         *      Map.Size.x
         *      
         * And world height
         *      Map.Size.y
         */

        public override Instruction GetInstruction(Vector2Int currentPos, Vector2Int targetPos)
        {
            // Delete this line before coding ...            
            System.Random rand = new System.Random();
            int val = rand.Next(4);
            if (val == 0) return Instruction.MoveLeft;
            else if (val == 1) return Instruction.MoveRight;
            else if (val == 2) return Instruction.MoveUp;
            else return Instruction.MoveDown;
        }

        // Don't modify anything below this line!

        public EnemyPathFinding(in PacmanGame.MapRaw map)
        {
            Map = map;
        }

        readonly private PacmanGame.MapRaw Map;
    }
}
