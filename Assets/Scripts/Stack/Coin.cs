using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private CoinDataSO _coinData;

    public Stack ParentStack { get; private set; }

    [SerializeField] private MeshRenderer _meshRend;

    [SerializeField] private TMP_Text _valueText;

    public int Value { get; private set; }

    public void Initialize(Stack stack)
    {
        ParentStack = stack;

        transform.SetParent(ParentStack.transform);

        SetRandomValue();

        SetColor();

        gameObject.SetActive(true);
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

    public void SetPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public async void MoveCoin(Stack targetStack)
    {

    }
}