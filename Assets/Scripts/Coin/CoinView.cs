using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Coin))]
public class CoinView : MonoBehaviour
{
    private const string RunBoolVariable = "IsDestroy";

    private Coin _coin;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _coin = GetComponent<Coin>();
    }

    private void OnEnable()
    {
        _coin.Collected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _coin.Collected -= OnCoinCollected;
    }

    public void OnDestroyAnimationEnd()
    {
        _coin.Destroy();
    }

    private void OnCoinCollected()
    {
        _animator.SetBool(RunBoolVariable, true);
    }
}