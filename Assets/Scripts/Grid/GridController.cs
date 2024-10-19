using System.Collections.Generic;
using UnityEngine;

public class GridController : Singleton<GridController>, IPoolable
{
    [SerializeField] private GridDataSO _gridData;

    private ObjectPool<Tile> _tilePool;
    [SerializeField] private Transform _tileContainerTransform;

    public Dictionary<Vector2, Tile> TileDictionary { get; private set; }

    public Vector2 GridSize { get { return new Vector2(_gridData.Width, _gridData.Height); } }

    protected override void Awake()
    {
        base.Awake();

        CreatePool();
    }
    private void Start()
    {
        InitializeGrid();
    }

    public void CreatePool()
    {
        _tilePool = new ObjectPool<Tile>(_gridData.TilePrefab, _gridData.InitialPoolSize, _tileContainerTransform);
    }

    private void InitializeGrid()
    {
        TileDictionary = new Dictionary<Vector2, Tile>();

        float xOffset = (_gridData.Width - 1) / 2f * (_gridData.CellSize + _gridData.Spacing);
        float zOffset = (_gridData.Height - 1) / 2f * (_gridData.CellSize + _gridData.Spacing);

        for (int i = 0; i < _gridData.Height; i++)
        {
            for (int j = 0; j < _gridData.Width; j++)
            {
                Tile tile = _tilePool.Get();
                Vector2 coord = new Vector2(j, i);
                tile.Initialize(coord);

                float xPos = j * (1 + _gridData.Spacing) - xOffset;
                float zPos = (_gridData.Height - 1 - i) * (1 + _gridData.Spacing) - zOffset;

                TileDictionary.Add(new Vector2(j, i), tile);

                tile.SetPosition(new Vector3(xPos, 0, zPos));

                if (_gridData.LockedTileCoords.Contains(coord))
                {
                    tile.LockTile();
                }
            }
        }
    }

    public Tile CheckColumnFreeTileRow(int column)
    {
        int lastRow = -1;

        for (int i = _gridData.Height - 1; i >= 0; i--)
        {
            if (TileDictionary[new Vector2(column, i)].IsLocked)
            {
                break;
            }

            if (TileDictionary[new Vector2(column, i)].IsFree())
            {
                lastRow = i;
            }
        }

        if (lastRow >= 0)
        {
            return TileDictionary[new Vector2(column, lastRow)];
        }
        else
        {
            return null;
        }
    }

    public Tile GetTileAt(Vector2 coord)
    {
        try
        {
            return TileDictionary[coord];
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public Vector3 GetTilePosition(int row, int column)
    {
        return TileDictionary[new Vector2(column, row)].transform.position;
    }
}
