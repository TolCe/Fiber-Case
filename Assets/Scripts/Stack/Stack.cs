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
            coin.Initialize(this);
            coin.SetPosition(StackController.Instance.StackData.Spacing * CoinList.Count * Vector3.up);

            AddCoin(coin);
        }
    }

    public void AddCoin(Coin coin)
    {
        CoinList.Add(coin);
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
}
