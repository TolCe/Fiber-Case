using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackData", menuName = "Stack/Stack Data")]
public class StackDataSO : ScriptableObject
{
    [SerializeField] private Coin _coinPrefab;
    public Coin CoinPrefab { get { return _coinPrefab; } }

    [SerializeField] private int _initialPoolSize = 20;
    public int InitialPoolSize { get { return _initialPoolSize; } }

    [SerializeField] private Stack _stackPrefab;
    public Stack StackPrefab { get { return _stackPrefab; } }

    [SerializeField] private float _spacing = 0.2f;
    public float Spacing { get { return _spacing; } }
}
