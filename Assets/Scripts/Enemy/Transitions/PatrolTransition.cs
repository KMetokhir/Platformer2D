
public class PatrolTransition : EnemyTransition
{
    protected override void Enable()
    {
        _detector.DamageableLost += OnDamageableLost;
    }

    private void OnDisable()
    {
        _detector.DamageableLost -= OnDamageableLost;
    }

    private void OnDamageableLost()
    {
        NeedTransit = true;
    }
}