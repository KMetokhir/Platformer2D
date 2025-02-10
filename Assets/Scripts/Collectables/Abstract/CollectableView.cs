using UnityEngine;

public abstract class CollectableView : MonoBehaviour
{
    [SerializeField] private CollectableAnimator _collectableAnimator;

    private Collectable _collectableModel;

    private void Awake()
    {
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
        _collectableAnimator.PlayDestroyAnimation();
    }
}