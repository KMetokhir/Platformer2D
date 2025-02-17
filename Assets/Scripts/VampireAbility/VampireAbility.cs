using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private VampireAria _vampireAria;
    [SerializeField] private Button _activateButton;
    [SerializeField] private Energy _energy;

    [SerializeField] private uint _suckPerIntervalValue;
    [SerializeField] private float _suckInterval;

    private IVampireTarget _currentTarget;

    private Coroutine _coroutine;

    private bool _isWorking = false;

    public event Action<uint> HealthSucked;

    private void OnEnable()
    {
        _vampireAria.TargetEntered += OnTargetEntered;
        _vampireAria.AllTargetsLost += OnAllTargetsLost;

        _activateButton.onClick.AddListener(OnActivateButtonClick);

        _energy.EnergyEmpty += OnEnergyEmpty;
        _energy.EnergyRecharged += OnEnergyRecharged;
    }

    private void OnDisable()
    {
        _vampireAria.TargetEntered -= OnTargetEntered;
        _vampireAria.AllTargetsLost -= OnAllTargetsLost;

        _activateButton.onClick.RemoveListener(OnActivateButtonClick);

        _energy.EnergyEmpty -= OnEnergyEmpty;
        _energy.EnergyRecharged -= OnEnergyRecharged;

        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void OnTargetEntered(IVampireTarget target)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(SmoothSuck(target));
        }
    }

    private void OnAllTargetsLost(IVampireTarget target)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _currentTarget = null;
    }

    private void OnActivateButtonClick()
    {
        _activateButton.interactable = false;
        _isWorking = true;
        _vampireAria.Activate();

        _energy.Use();
    }

    private void OnEnergyEmpty()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        _currentTarget = null;
        _activateButton.interactable = false;
        _vampireAria.Deactivate();
        _isWorking = false;
    }

    private void OnEnergyRecharged()
    {
        _activateButton.interactable = true;
    }

    private IEnumerator SmoothSuck(IVampireTarget target)
    {
        WaitForSeconds waitingTime = new WaitForSeconds(_suckInterval);
        _currentTarget = target;

        while (_isWorking)
        {
            if (_vampireAria.TryGetClosestTarget(out IVampireTarget closestTarget))
            {
                if (_currentTarget != closestTarget)
                {
                    _currentTarget = closestTarget;
                }
            }

            uint suckValue = _currentTarget.Suck(_suckPerIntervalValue);
            HealthSucked?.Invoke(suckValue);

            yield return waitingTime;
        }
    }
}