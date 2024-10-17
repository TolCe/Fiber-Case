using UnityEngine;

public class GridController : Singleton<GridController>
{
    [SerializeField] private GridDataSO _gridData;

    private ObjectPool<Tile> _tilePool;
    [SerializeField] private Transform _tileContainerTransform;

    protected override void Awake()
    {
        base.Awake();

        CreatePool();
    }
    private void Start()
    {
        InitializeGrid();
    }

    private void CreatePool()
    {
        _tilePool = new ObjectPool<Tile>(_gridData.TilePrefab, _gridData.InitialPoolSize, _tileContainerTransform);
    }

    private void InitializeGrid()
    {
        float xOffset = (_gridData.Width - 1) / 2f * (1 + _gridData.Spacing);
        float zOffset = (_gridData.Height - 1) / 2f * (1 + _gridData.Spacing);

        for (int i = 0; i < _gridData.Height; i++)
        {
            for (int j = 0; j < _gridData.Width; j++)
            {
                Tile tile = _tilePool.Get();
                tile.Initialize(new int[] { i, j });

                float xPos = j * (1 + _gridData.Spacing) - xOffset;
                float zPos = i * (1 + _gridData.Spacing) - zOffset;

                tile.SetPosition(new Vector3(xPos, 0, zPos));
            }
        }
    }
}
