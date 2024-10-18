using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinDataSO _coinData;

    [SerializeField] private MeshRenderer _meshRend;

    [SerializeField] private TMP_Text _valueText;

    public int Value { get; private set; }

    public Stack AttachedStack { get; private set; }

    public void Initialize()
    {
        SetRandomValue();

        SetColor();

        gameObject.SetActive(true);
    }

    public void AttachToStack(Stack stack)
    {
        AttachedStack = stack;
        transform.SetParent(AttachedStack.transform);
    }

    private void SetRandomValue()
    {
        SetValue(Random.Range(1, _coinData.ColorsByValue.Length + 1));
    }

    private void SetValue(int value)
    {
        Value = value;
        _valueText.text = $"{Value}";
    }

    private void SetColor()
    {
        _meshRend.material = _coinData.ColorsByValue[Value - 1];
    }

    public async Task SetPosition(Vector3 pos, float duration)
    {
        await transform.DOLocalMove(pos, duration).AsyncWaitForCompletion();
    }

    public async Task MoveCoin(Stack targetStack)
    {
        AttachedStack.RemoveCoin(this);
        await targetStack.AddCoin(this, _coinData.MoveDuration);
    }
}