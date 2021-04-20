using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Globals : MonoBehaviour
{
    public static Globals Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        GetTileBase = new Dictionary<CellFormat, MapTileBase>(){
            { CellFormat.Space,  MapTileBase.space},
            { CellFormat.Wall, MapTileBase.wall }
        };
    }

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
    }
    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
    }

    public const int CELL_SIZE_H = 8;
    public const int CELL_SIZE_V = 8;

    public const int PIXELS_PER_UNIT = 8;

    public const float PlayerMoveTime = 0.2f;

    public Dictionary<CellFormat, MapTileBase> GetTileBase;
    public enum CellFormat
    {
        Wall = '#',
        Space = '0'
    }

    [SerializeField] public MapRaw CurrentMapRaw;
    [SerializeField] public Tilemap CurrentTilemap;
    [SerializeField] public PlayerController Player;

    private void OnGameStart()
    {
        
    }
}
