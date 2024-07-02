using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class CollectableView : MonoBehaviour
{
    private const string RunBoolVariable = "IsDestroy";

    private Collectable _collectableModel;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collectableModel = GetComponent<Collectable>();
    }

    private void OnEnable()
    {
        _collectableModel.Collected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _collectableModel.Collected -= OnCoinCollected;
    }

    public void OnDestroyAnimationEnd()
    {
        _collectableModel.Destroy();
    }

    private void OnCoinCollected()
    {
        _animator.SetBool(RunBoolVariable, true);
    }
}