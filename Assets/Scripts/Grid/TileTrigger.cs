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
        StackMoveController.Instance.TryMoveMain(StackShowcase.Instance.MainStack, _tile.Coordinates);
    }
}
