using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{
    [SerializeField] private Button _switchStackButton;

    private void Start()
    {
        _switchStackButton.onClick.AddListener(OnSwitchStackButtonPressed);
    }

    private async void OnSwitchStackButtonPressed()
    {
        _switchStackButton.interactable = false;
        await StackShowcase.Instance.SwitchStacks();
        _switchStackButton.interactable = true;
    }
}
