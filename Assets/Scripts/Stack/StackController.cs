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

    public List<Stack> ActiveStackList { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        CreatePool();

        ActiveStackList = new List<Stack>();
    }

    public void CreatePool()
    {
        _coinPool = new ObjectPool<Coin>(_stackData.CoinPrefab, _stackData.InitialPoolSize, _coinContainerTransform);
        _stackPool = new ObjectPool<Stack>(_stackData.StackPrefab, _stackData.InitialPoolSize, _stackContainerTransform);
    }

    public Coin GenerateNewCoin(int value = -1)
    {
        Coin coin = _coinPool.Get();
        coin.Initialize(value);
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

    public void DiscardStack(Stack stack, Vector2 coord)
    {
        _stackPool.Return(stack);

        for (int i = (int)coord.x; i >= 0; i--)
        {
            StackMoveController.Instance.TryMove(GridController.Instance.GetTileAt(new Vector2(i, coord.y)).AttachedStack, coord);
        }

        ActiveStackList.Remove(stack);
    }

    public void ActivateStack(Stack stack)
    {
        if (ActiveStackList.Contains(stack))
        {
            return;
        }

        ActiveStackList.Add(stack);
    }

    public void CheckForCoinUpgrades(Stack stack)
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
            stack.GenerateNewCoin(-1f, oldValue + 1);
        }
    }
}