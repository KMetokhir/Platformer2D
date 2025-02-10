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

    private void Update()
    {
        if (_currentState == null)
            return;

        EnemyState nextState = _currentState.GetNextState();

        if (nextState)
            Transit(nextState);
    }

    private void Transit(EnemyState nextState)
    {
        if (_currentState)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState)
        {
            _currentState.Enter();
        }
    }
}