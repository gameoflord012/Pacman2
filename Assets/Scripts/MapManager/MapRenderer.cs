using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Assertions;

[RequireComponent(typeof(Tilemap))]
public class MapRenderer : MonoBehaviour
{
    [SerializeField] Vector2Int offSet;

    MapRaw mapRaw;
    Tilemap tileMap;

    private void Start()
    {
        tileMap = GetComponent<Tilemap>();
        mapRaw = Globals.Instance.CurrentMapRaw;
        DrawOnTilemap();
    }

    private void DrawOnTilemap()
    {
        var globals = Globals.Instance;
        var size = mapRaw.Size;        

        for (int i = 0; i < size.x; i++)
            for (int j = 0; j < size.y; j++)
            {                
                foreach(var e in globals.GetTileBase) 
                    if(e.Key == mapRaw.GetTile(i, j))
                    {
                        tileMap.SetTile(new Vector3Int(j + offSet.x, size.x - 1 - i + offSet.y, 0), e.Value);
                        break;
                    }
            }
    }
}
