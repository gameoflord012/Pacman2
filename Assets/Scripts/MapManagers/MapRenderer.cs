using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Assertions;

namespace PacmanGame {
    [RequireComponent(typeof(Tilemap))]
    public class MapRenderer : MonoBehaviour
    {
        private void Start()
        {
            _tileMap = GetComponent<Tilemap>();
            MapRaw = Globals.Instance.CurrentMapRaw;
            DrawOnTilemap();
        }

        private void DrawOnTilemap()
        {
            var globals = Globals.Instance;
            var size = MapRaw.Size;

            for (int i = 0; i < size.x; i++)
                for (int j = 0; j < size.y; j++)
                {
                    foreach (var e in globals.GetTileBase)
                        if (e.Key == MapRaw.GetTile(i, j))
                        {
                            _tileMap.SetTile(new Vector3Int(j + _offSet.x, size.x - 1 - i + _offSet.y, 0), e.Value);
                            break;
                        }
                }
        }

        [SerializeField] private Vector2Int _offSet;
        MapRaw MapRaw; // Mark as readonly
        Tilemap _tileMap;
    }
}
