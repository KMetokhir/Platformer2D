using System;
using System.Data;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private EnemyState _startState;

    private EnemyState _currentState;

    private void Start()
    {
        _currentState = _startState;
        _currentState.Enter();
    }

    public void Transit(EnemyState stateInvoker, EnemyState nextState)
    {
        if(_currentState!=stateInvoker && _currentState!=null)
        {
            throw new Exception($"State Invoker {stateInvoker.name} is not current active state  {_currentState.name}");
        }

        if (_currentState)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState)
        {
            _currentState.Enter();
        }
    }
}