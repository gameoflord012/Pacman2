using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// MapForm is a map holder containing basic information of the world
/// </summary>
/// 
namespace PacmanGame
{
    [CreateAssetMenu(fileName = "New Map")]
    public class MapRaw : ScriptableObject
    {

        [SerializeField] private Vector2Int size;
        [SerializeField] private Vector2Int playerPos;
        [SerializeField] private Vector2Int ghostPos;

        public Vector2Int Size { get { return size; } private set { } }
        public Vector2Int PlayerPos { get { return playerPos; } private set { } }
        public Vector2Int GhostPos { get { return ghostPos; } private set { } }


        private void OnEnable()
        {
            _isLayoutChecked = false;
            _formatedLayout = "";
        }

        [TextArea(20, 20)] [SerializeField] string layout;
        private string FormatedLayout
        {
            get
            {
                if (!_isLayoutChecked)
                {
                    _isLayoutChecked = true;
                    _formatedLayout = Reformat(layout);
                    CheckValid();
                }
                return _formatedLayout;
            }
        }

        public string Layout
        {
            get
            {
                return layout;
            }
            private set
            {
                layout = value;
                _isLayoutChecked = false;
            }
        }

        public Globals.CellFormat GetTile(int x, int y)
        {
            return (Globals.CellFormat)FormatedLayout[x * size.y + y];
        }

        public bool IsWall(int x, int y)
        {
            return GetTile(x, y) == Globals.CellFormat.Wall;
        }

        public bool IsSpace(int x, int y)
        {
            return GetTile(x, y) == Globals.CellFormat.Space;
        }

        private void CheckValid()
        {
            // Check Layout
            if (_formatedLayout.Length != size.x * size.y)
                throw new InvalidMapFormat("Format of the map and map size is not compatible.");

            foreach (Globals.CellFormat c in _formatedLayout)
            {
                if (c != Globals.CellFormat.Wall && c != Globals.CellFormat.Space)
                    throw new InvalidMapFormat("Format contain weird characters.");
            }

            // Check Valid Spawn Point
            if (playerPos.x == ghostPos.x && playerPos.y == ghostPos.y)
                throw new InvalidMapFormat("Ghost and player position shouldn't be the same.");

            if (GetTile(playerPos.x, playerPos.y) == Globals.CellFormat.Wall ||
                playerPos.x < 0 || playerPos.x >= size.x ||
                playerPos.y < 0 || playerPos.y >= size.y)
            {
                throw new InvalidMapFormat("Invalid player spawn position.");
            }

            if (GetTile(ghostPos.x, ghostPos.y) == Globals.CellFormat.Wall ||
                ghostPos.x < 0 || ghostPos.x >= size.x ||
                ghostPos.y < 0 || ghostPos.y >= size.y)
            {
                throw new InvalidMapFormat("Invalid ghost spawn position.");
            }
        }
        private static string Reformat(string s)
        {
            s = s.Replace("\n", "");
            s = s.Replace("\r", "");
            s = s.Replace("\t", "");
            return s;
        }

        private bool _isLayoutChecked;
        private string _formatedLayout;
    }
}

