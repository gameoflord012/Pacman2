using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// MapCell is a custom TileBase for the TileMap
/// </summary>
public class MapTileBase : TileBase
{
    public static MapTileBase wall = Factory(Sprite.Create(
        ResourceManager.Instance.WallTexture, 
        new Rect(0, 0, Globals.CELL_SIZE_H, Globals.CELL_SIZE_V), 
        new Vector2(0.5f, 0.5f), Globals.PIXELS_PER_UNIT));

    public static MapTileBase space = Factory(Sprite.Create(
        ResourceManager.Instance.SpaceTexture,
        new Rect(0, 0, Globals.CELL_SIZE_H, Globals.CELL_SIZE_V),
        new Vector2(0.5f, 0.5f), Globals.PIXELS_PER_UNIT));

    Sprite image;

    public static MapTileBase Factory(Sprite _image)
    {
        MapTileBase instance = ScriptableObject.CreateInstance("MapTileBase") as MapTileBase;
        instance.image = _image;
        return instance;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = image;
    }
}