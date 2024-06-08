using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyPlate;
    [SerializeField] private Wallet _wallet;

    private void OnEnable()
    {
        _wallet.MoneyValueChanged += OnMoneyValueChanged;
    }

    private void OnDisable()
    {
        _wallet.MoneyValueChanged -= OnMoneyValueChanged;             
    }

    private void OnMoneyValueChanged(uint value)
    {
        _moneyPlate.text = value.ToString();
    }
}
