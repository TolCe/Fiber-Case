using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Stack : MonoBehaviour
{
    public List<Coin> CoinList { get; private set; }

    public Tile AttachedTile { get; private set; }

    public void Initialize(Transform parent)
    {
        CoinList = new List<Coin>();

        transform.position = parent.transform.position;

        gameObject.SetActive(true);

        GenerateRandomCoins();
    }

    private void GenerateRandomCoins()
    {
        int coinAmount = Random.Range(1, 6);

        for (int i = 0; i < coinAmount; i++)
        {
            Coin coin = StackController.Instance.GetCoinFromPool();
            AddCoin(coin);
            coin.Initialize();
        }
    }

    public async Task AddCoin(Coin coin, float moveDuration = 0f)
    {
        CoinList.Add(coin);
        coin.AttachToStack(this);
        await coin.SetPosition(StackController.Instance.StackData.Spacing * (CoinList.Count - 1) * Vector3.up, moveDuration);
    }

    public void RemoveCoin(Coin coin)
    {
        CoinList.Remove(coin);

        if (CoinList.Count == 0)
        {
            DestroyStack();
        }
    }

    public Coin GetTopCoin()
    {
        if (CoinList.Count <= 0)
        {
            return null;
        }

        return CoinList[CoinList.Count - 1];
    }

    public async Task MoveStack(Vector3 targetPos)
    {
        await transform.DOMove(targetPos, 0.4f).AsyncWaitForCompletion();
    }

    public void AttachToTile(Tile tile)
    {
        AttachedTile = tile;
        tile.AttachStack(this);
    }

    private void DestroyStack()
    {
        AttachedTile.ResetStack();
        StackController.Instance.ReturnStackToPool(this);
    }
}
