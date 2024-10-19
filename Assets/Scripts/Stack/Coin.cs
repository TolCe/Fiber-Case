using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinDataSO _coinData;

    [SerializeField] private MeshRenderer _meshRend;

    [SerializeField] private TMP_Text _valueText;

    public int Value { get; private set; }

    public Stack AttachedStack { get; private set; }

    public void Initialize(int value)
    {
        SetValue(value);

        SetColor();

        gameObject.SetActive(true);
    }

    public void AttachToStack(Stack stack)
    {
        AttachedStack?.RemoveCoin(this);

        AttachedStack = stack;
        transform.SetParent(AttachedStack.transform);
    }

    private void SetRandomValue()
    {
        SetValue(Random.Range(1, _coinData.ColorsByValue.Length + 1));
    }

    private void SetValue(int value)
    {
        if (value < 0)
        {
            SetRandomValue();

            return;
        }

        Value = value;
        _valueText.text = $"{Value}";
    }

    private void SetColor()
    {
        _meshRend.material = _coinData.ColorsByValue[Mathf.Clamp(Value - 1, 0, _coinData.ColorsByValue.Length)];
    }

    private async Task SetPosition(Vector3 pos, float duration)
    {
        await transform.DOMove(pos, duration).AsyncWaitForCompletion();
    }

    public async Task MoveCoin(Stack targetStack, float moveDuration)
    {
        if (moveDuration != 0)
        {
            moveDuration = _coinData.MoveDuration;
        }

        await SetPosition(targetStack.transform.position + (0.2f + StackController.Instance.StackData.Spacing * (targetStack.CoinList.Count - 1)) * Vector3.up, moveDuration);

        targetStack.AddCoin(this);

        await Task.Delay(0);
    }
}