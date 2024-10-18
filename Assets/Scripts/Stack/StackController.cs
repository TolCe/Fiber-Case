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

    public Coin GetCoinFromPool()
    {
        return _coinPool.Get();
    }

    public Stack GetStackFromPool()
    {
        return _stackPool.Get();
    }
}