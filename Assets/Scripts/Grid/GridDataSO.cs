using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridData", menuName = "Grid/Grid Data")]
public class GridDataSO : ScriptableObject
{
    [SerializeField] private int _width = 6;
    public int Width { get { return _width; } }

    [SerializeField] private int _height = 7;
    public int Height { get { return _height; } }

    [SerializeField] private Tile _tilePrefab;
    public Tile TilePrefab { get { return _tilePrefab; } }

    [SerializeField] private int _initialPoolSize = 20;
    public int InitialPoolSize { get { return _initialPoolSize; } }

    [SerializeField] private float _cellSize = 1f;
    public float CellSize { get { return _cellSize; } }

    [SerializeField] private float _spacing = 0.1f;
    public float Spacing { get { return _spacing; } }

    [SerializeField] private List<Vector2> _lockedTileCoords;
    public List<Vector2> LockedTileCoords { get { return _lockedTileCoords; } }
}