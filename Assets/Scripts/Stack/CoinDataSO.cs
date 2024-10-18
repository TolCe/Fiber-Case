using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Stack/Coin Data")]
public class CoinDataSO : ScriptableObject
{
    [SerializeField] private Material[] _colorsByValue;
    public Material[] ColorsByValue { get { return _colorsByValue; } }
}