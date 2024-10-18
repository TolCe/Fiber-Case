using UnityEngine;

public class GridController : Singleton<GridController>, IPoolable
{
    [SerializeField] private GridDataSO _gridData;

    private ObjectPool<Tile> _tilePool;
    [SerializeField] private Transform _tileContainerTransform;

    private Tile[,] _tiles;

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
        _tiles = new Tile[_gridData.Height, _gridData.Width];

        float xOffset = (_gridData.Width - 1) / 2f * (_gridData.CellSize + _gridData.Spacing);
        float zOffset = (_gridData.Height - 1) / 2f * (_gridData.CellSize + _gridData.Spacing);

        for (int i = 0; i < _gridData.Height; i++)
        {
            for (int j = 0; j < _gridData.Width; j++)
            {
                Tile tile = _tilePool.Get();
                tile.Initialize(new int[] { i, j });

                float xPos = j * (1 + _gridData.Spacing) - xOffset;
                float zPos = i * (1 + _gridData.Spacing) - zOffset;

                _tiles[i, j] = tile;

                tile.SetPosition(new Vector3(xPos, 0, zPos));
            }
        }
    }

    public Tile CheckColumnFreeTileRow(int column)
    {
        int lastRow = -1;

        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            if (_tiles[i, column].IsFree())
            {
                lastRow = i;
            }
        }

        if (lastRow >= 0)
        {
            return _tiles[lastRow, column];
        }
        else
        {
            return null;
        }
    }

    public Tile GetTileAt(int row, int column)
    {
        try
        {
            return _tiles[row, column];
        }
        catch (System.Exception)
        {
            return null;
        }
    }

    public Vector3 GetTilePosition(int row, int column)
    {
        return _tiles[row, column].transform.position;
    }
}
