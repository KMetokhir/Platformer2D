using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private uint _money;

    public event Action<uint> MoneyValueChanged;

    public void IncreaseMoney(uint value)
    {
        _money += value;
        MoneyValueChanged?.Invoke(_money);
    }
}