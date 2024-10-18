using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTrigger : MonoBehaviour
{
    private Tile _tile;

    public void Initialize(Tile tile)
    {
        _tile = tile;
    }

    private void OnMouseDown()
    {
        StackMoveController.Instance.TryMove(_tile.Coordinates[1]);
    }
}
