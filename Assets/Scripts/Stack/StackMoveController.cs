using UnityEngine;

public class StackMoveController : Singleton<StackMoveController>
{
    [SerializeField] private StackDataSO _stackData;

    public async void TryMove(int column)
    {
        Stack stack = StackShowcase.Instance.MainStack;
        Tile targetTile = GridController.Instance.CheckColumnFreeTileRow(column);

        if (targetTile == null)
        {
            return;
        }

        await stack.MoveStack(targetTile.transform.position);

        stack.AttachToTile(targetTile);

        StackShowcase.Instance.OnMainStackMoved();
    }

    public Vector3 CalculateTargetPosition(Stack stack)
    {
        Vector3 targetPosition = _stackData.Spacing * stack.CoinList.Count * Vector3.up;

        return targetPosition;
    }
}