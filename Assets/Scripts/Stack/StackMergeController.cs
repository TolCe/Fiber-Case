using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class StackMergeController : Singleton<StackMergeController>
{
    [SerializeField] private List<Vector2> _neighbourCoordList = new List<Vector2>() { Vector2.right, Vector2.left, Vector2.up, Vector2.down };

    public async Task CheckNeighboursForMerge(Stack stack)
    {
        List<Stack> stacksToCheckList = new List<Stack>() { stack };
        List<Stack> modifiedStacksList = new List<Stack>();

        foreach (Vector2 coord in _neighbourCoordList)
        {
            Vector2 targetCoord = coord;
            try
            {
                targetCoord += stack.AttachedTile.Coordinates;
            }
            catch (System.Exception)
            {
                return;
            }

            Stack neighbourStack = GridController.Instance.GetTileAt(targetCoord)?.AttachedStack;

            if (neighbourStack != null)
            {
                List<Coin> matchedCoinList = GatherUntilNoMatch(stack, neighbourStack);
                if (matchedCoinList.Count > 0)
                {
                    modifiedStacksList.Add(neighbourStack);
                    await MergeCoins(matchedCoinList, stack);
                }
            }
        }

        StackController.Instance.CheckForCoinUpgrades(stack);

        foreach (Stack modifiedStack in modifiedStacksList)
        {
            await CheckNeighboursForMerge(modifiedStack);
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
            await coin.MoveCoin(targetStack, -1);
        }
    }
}