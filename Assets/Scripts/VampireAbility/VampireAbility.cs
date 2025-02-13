using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VampireAbility : MonoBehaviour
{
    [SerializeField] private VampireAria _vampireAria;
    [SerializeField] private Button _activateButton;
    [SerializeField] private Energy _energy;

    [SerializeField] private uint _suckPerIntervalValue;
    [SerializeField] private float _suckInterval;

    private List<TargetCoroutinePair> _targetsInAria = new List<TargetCoroutinePair>();

    private bool _isWorking = false;

    public event Action<uint> HealthSucked;

    private void OnEnable()
    {
        _vampireAria.TargetDetected += OnTargetDetected;
        _vampireAria.TargetLost += OnTargetLost;

        _activateButton.onClick.AddListener(OnActivateButtonClick);

        _energy.EnergyEmpty += OnEnergyEmpty;
        _energy.EnergyRecharged += OnEnergyRecharged;
    }

    private void OnDisable()
    {
        _vampireAria.TargetDetected -= OnTargetDetected;
        _vampireAria.TargetLost -= OnTargetLost;

        _activateButton.onClick.RemoveListener(OnActivateButtonClick);

        _energy.EnergyEmpty -= OnEnergyEmpty;
        _energy.EnergyRecharged -= OnEnergyRecharged;

        StopAllCoroutines(_targetsInAria);
    }

    private void StopAllCoroutines(List<TargetCoroutinePair> targetsInAria)
    {
        foreach (TargetCoroutinePair target in targetsInAria)
        {
            target.Stop(this);
        }
    }

    private void OnTargetDetected(IVampireTarget target)
    {
        Coroutine coroutine = StartCoroutine(SmoothSuck(target));

        TargetCoroutinePair targetCoroutinePair = new TargetCoroutinePair(target, coroutine);

        _targetsInAria.Add(targetCoroutinePair);
    }

    private void OnTargetLost(IVampireTarget target)
    {
        TargetCoroutinePair pairInAria = _targetsInAria.Find(x => x.Equals(target));
        pairInAria.Stop(this);

        _targetsInAria.Remove(pairInAria);
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

        while (_isWorking)
        {
            uint suckValue = target.Suck(_suckPerIntervalValue);
            HealthSucked?.Invoke(suckValue);

            yield return waitingTime;
        }
    }
}