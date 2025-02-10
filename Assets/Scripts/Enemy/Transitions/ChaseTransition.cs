
public class ChaseTransition : EnemyTransition
{
    protected override void Enable()
    {
        _detector.DamageableDetected += OnDamageableFound;
    }

    private void OnDisable()
    {
        _detector.DamageableDetected -= OnDamageableFound;
    }

    private void OnDamageableFound(IDamageable damageable)
    {
        NeedTransit = true;
    }
}