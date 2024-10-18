using UnityEngine;

[CreateAssetMenu(fileName = "CoinData", menuName = "Stack/Coin Data")]
public class CoinDataSO : ScriptableObject
{
    [SerializeField] private Material[] _colorsByValue;
    public Material[] ColorsByValue { get { return _colorsByValue; } }

    [SerializeField] private float _moveDuration = 0.25f;
    public float MoveDuration { get { return _moveDuration; } }
}