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

    private async void GenerateRandomCoins()
    {
        int coinAmount = Random.Range(1, 6);

        for (int i = 0; i < coinAmount; i++)
        {
            await GenerateNewCoin(0f);
        }
    }

    public async Task GenerateNewCoin(float moveDuration = -1f, int value = -1)
    {
        Coin coin = StackController.Instance.GenerateNewCoin(value);

        await coin.MoveCoin(this, moveDuration);

        coin.Initialize(value);

        await Task.Delay(0);
    }

    public void AddCoin(Coin coin)
    {
        CoinList.Add(coin);
        coin.AttachToStack(this);
    }

    public void RemoveCoin(Coin coin)
    {
        CoinList.Remove(coin);

        if (CoinList.Count <= 0)
        {
            DestroyStack(AttachedTile.Coordinates);
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
        ResetStack();
        await transform.DOMove(targetPos, 0.4f).AsyncWaitForCompletion();
    }

    public void AttachToTile(Tile tile)
    {
        AttachedTile = tile;
        tile.AttachStack(this);
    }

    private void DestroyStack(Vector2 coord)
    {
        ResetStack();
        StackController.Instance.DiscardStack(this, coord);
    }

    private void ResetStack()
    {
        AttachedTile?.ResetTile();
        AttachedTile = null;
    }
}
