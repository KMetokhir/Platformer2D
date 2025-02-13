using UnityEngine;

public class TargetCoroutinePair
{
    private IVampireTarget _target;
    private Coroutine _coroutine;

    public TargetCoroutinePair(IVampireTarget target, Coroutine coroutine)
    {
        _target = target;
        _coroutine = coroutine;
    }

    public void Stop(MonoBehaviour owner)
    {
        if (_coroutine != null)
        {
            owner.StopCoroutine(_coroutine);
        }
    }

    public override int GetHashCode()
    {
        return _target.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj is TargetCoroutinePair || obj is IVampireTarget && obj != null)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        return false;
    }
}