using System.Collections.Generic;
using UnityEngine;

public class StackController : Singleton<StackController>, IPoolable
{
    [SerializeField] private StackDataSO _stackData;
    public StackDataSO StackData { get { return _stackData; } }

    private ObjectPool<Coin> _coinPool;
    [SerializeField] private Transform _coinContainerTransform;

    private ObjectPool<Stack> _stackPool;
    [SerializeField] private Transform _stackContainerTransform;

    protected override void Awake()
    {
        base.Awake();

        CreatePool();
    }

    public void CreatePool()
    {
        _coinPool = new ObjectPool<Coin>(_stackData.CoinPrefab, _stackData.InitialPoolSize, _coinContainerTransform);
        _stackPool = new ObjectPool<Stack>(_stackData.StackPrefab, _stackData.InitialPoolSize, _stackContainerTransform);
    }

    public Coin GenerateNewCoin(int value = -1)
    {
        Coin coin = _coinPool.Get();
        return coin;
    }

    public void DiscardCoin(Coin coin)
    {
        coin.AttachedStack.RemoveCoin(coin);
        _coinPool.Return(coin);
    }

    public Stack GetStackFromPool()
    {
        return _stackPool.Get();
    }

    public async void DiscardStack(Stack stack, Vector2 coord)
    {
        _stackPool.Return(stack);

        for (int i = (int)coord.y + 1; i < GridController.Instance.GridSize.y; i++)
        {
            await StackMoveController.Instance.TryMove(GridController.Instance.GetTileAt(new Vector2(coord.x, i)).AttachedStack, coord);
        }
    }

    public async void CheckForCoinUpgrades(Stack stack)
    {
        List<Coin> sameCoinList = new List<Coin>() { stack.CoinList[stack.CoinList.Count - 1] };
        for (int i = stack.CoinList.Count - 2; i >= 0; i--)
        {
            if (stack.CoinList[i].Value == sameCoinList[0].Value)
            {
                sameCoinList.Add(stack.CoinList[i]);
            }
            else
            {
                break;
            }
        }

        if (sameCoinList.Count >= _stackData.MinAmountToUpgrade)
        {
            int oldValue = stack.CoinList[stack.CoinList.Count - 1].Value;
            foreach (Coin coin in sameCoinList)
            {
                DiscardCoin(coin);
            }
            await stack.GenerateNewCoin(-1f, oldValue + 1);
            await StackMergeController.Instance.CheckNeighboursForMerge(stack);
        }
    }
}