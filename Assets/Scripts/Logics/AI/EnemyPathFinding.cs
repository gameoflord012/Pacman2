using System;
using UnityEngine;

namespace GameAI
{

    public class EnemyPathFinding : IPathFinding
    {
        /* Start coding here ...
         * 
         * map.GetTile(x, y) to retrive informations at tile (x, y)
         * 
         * To check if a tile is wall
         *      Map.IsWall(x, y) == true
         *      
         * To check if a tile is space
         *      Map.IsSpace(x, y) == true
         *      
         * To get world width
         *      Map.Size.x
         *      
         * And world height
         *      Map.Size.y
         *      
         * The pivot is on the top left corner and position at (0, 0)
         */

        public override Instruction GetInstruction(Vector2Int currentPos, Vector2Int targetPos)
        {
            // Delete these before coding ...
            int w = Map.Size.x;
            int h = Map.Size.y;

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
