using System.Threading.Tasks;
using UnityEngine;

public class StackMoveController : Singleton<StackMoveController>
{
    [SerializeField] private StackDataSO _stackData;

    public bool MovementDone { get; private set; }

    private void Start()
    {
        MovementDone = true;
    }

    public async void TryMoveMain(Stack stack, Vector2 coord)
    {
        if (!MovementDone)
        {
            return;
        }

        StackShowcase.Instance.OnMainStackMoved();
        await TryMove(stack, coord);
    }

    public async Task TryMove(Stack stack, Vector2 coord)
    {
        Tile targetTile = GridController.Instance.CheckColumnFreeTileRow((int)coord.y);

        if (targetTile == null)
        {
            return;
        }

        MovementDone = false;

        await stack.MoveStack(targetTile.transform.position);

        stack.AttachToTile(targetTile);

        await StackMergeController.Instance.CheckNeighboursForMerge(stack);

        MovementDone = true;
    }

    public Vector3 CalculateTargetPosition(Stack stack)
    {
        Vector3 targetPosition = _stackData.Spacing * stack.CoinList.Count * Vector3.up;

        return targetPosition;
    }
}