using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StackMergeController : Singleton<StackMergeController>
{
    public async void CheckHorizontalNeighboursForMerge(Stack stack)
    {
        int row = stack.AttachedTile.Coordinates[0];
        int column = stack.AttachedTile.Coordinates[1] - 1;
        Tile tile = GridController.Instance.GetTileAt(row, column);

        if (tile?.AttachedStack != null)
        {
            List<Coin> matchedCoinList = GatherUntilNoMatch(stack, tile.AttachedStack);
            await MergeCoins(matchedCoinList, stack);
        }

        column = stack.AttachedTile.Coordinates[1] + 1;
        tile = GridController.Instance.GetTileAt(row, column);

        if (tile?.AttachedStack != null)
        {
            List<Coin> matchedCoinList = GatherUntilNoMatch(stack, tile.AttachedStack);
            await MergeCoins(matchedCoinList, stack);
        }

        row = stack.AttachedTile.Coordinates[0] + 1;
        column = stack.AttachedTile.Coordinates[1];
        tile = GridController.Instance.GetTileAt(row, column);

        if (tile?.AttachedStack != null)
        {
            List<Coin> matchedCoinList = GatherUntilNoMatch(tile.AttachedStack, stack);
            await MergeCoins(matchedCoinList, tile.AttachedStack);
        }
    }

    private List<Coin> GatherUntilNoMatch(Stack gathererStack, Stack gatheredStack)
    {
        List<Coin> matchedCoinList = new List<Coin>();

        for (int i = gatheredStack.CoinList.Count - 1; i >= 0; i--)
        {
            if (gatheredStack.CoinList[i].Value == gathererStack.GetTopCoin().Value)
            {
                matchedCoinList.Add(gatheredStack.CoinList[i]);
            }
            else
            {
                break;
            }
        }

        return matchedCoinList;
    }

    private async Task MergeCoins(List<Coin> coinList, Stack targetStack)
    {
        foreach (Coin coin in coinList)
        {
            await coin.MoveCoin(targetStack);
        }
    }
}