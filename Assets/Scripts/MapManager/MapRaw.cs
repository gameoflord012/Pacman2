using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// MapForm is a map holder containing basic information of the world
/// </summary>
/// 
[CreateAssetMenu(fileName = "New Map")]
public class MapRaw : ScriptableObject
{    

    [SerializeField] Vector2Int size;
    [SerializeField] Vector2Int playerPos;
    [SerializeField] Vector2Int ghostPos;

    public Vector2Int Size { get { return size; } set { size = value; } }
    public Vector2Int PlayerPos { get { return playerPos; } set { playerPos = value; } }
    public Vector2Int GhostPos { get { return ghostPos; } set { ghostPos = value; } }


    private void OnEnable()
    {
        isLayoutChecked = false;
        formatedLayout = "";
    }

    [TextArea(20, 20)] [SerializeField] string layout;
    public string FormatedLayout
    {
        get
        {
            if (!isLayoutChecked) {
                isLayoutChecked = true;
                formatedLayout = Reformat(layout);
                CheckValid();
            }
            return formatedLayout;
        }
        private set { }
    }
    
    public string Layout
    { 
        get {
            return layout;
        } 
        set { 
            layout = value;
            isLayoutChecked = false;
        } 
    }        
    
    public Globals.CellFormat GetTile(int x, int y)
    {
        return (Globals.CellFormat) FormatedLayout[x * size.y + y];
    }    

    private void CheckValid()
    {
        // Check Layout
        if (formatedLayout.Length != size.x * size.y)
            throw new InvalidMapFormat("Format of the map and map size is not compatible.");

        foreach (Globals.CellFormat c in formatedLayout)
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

    private bool isLayoutChecked;
    private string formatedLayout;
}

