using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _firstState;
    [SerializeField] private MoveState _stateMove;
    [SerializeField] private State _stateDie;
    [SerializeField] private State _stateAttack;
    [SerializeField] private State _stateRestart;
    [SerializeField] private State _stateRestartScene;
    [SerializeField] private State _stateComplite;
    [SerializeField] private ColliderHandler _colliderHandler;
    [SerializeField] private Player _player;
    [SerializeField] private Score _score;
    [SerializeField] private Scene _Scene;
    //[SerializeField] private YandexReward _reward;

    private State _currentState;
    private bool _isAttack = false;
    private bool _isComplete = false;

    private void OnEnable()
    {
        _Scene.Started += SetStateRestart;
        _Scene.Restarted += SetStateRestartScene;
        _colliderHandler.Dead += SetStateDie;
        _colliderHandler.Caught += SetStateAttack;
        _colliderHandler.Moved += SetStateMove;
        _colliderHandler.Restarted += SetStateRestart;
        _player.Dead += SetStateAttack;
        _player.Won += SetStateComplite;
        //_reward.CustomizedPlatform -= ChangeSpeedForMobilePlatform;
    }

    private void Start()
    {
        ChangeState(_firstState);
    }

    private void OnDisable()
    {
        _Scene.Started -= SetStateRestart;
        _Scene.Restarted -= SetStateRestartScene;
        _colliderHandler.Dead -= SetStateDie;
        _colliderHandler.Caught -= SetStateAttack;
        _colliderHandler.Moved -= SetStateMove;
        _colliderHandler.Restarted -= SetStateRestart;
        _player.Dead -= SetStateAttack;
        _player.Won -= SetStateComplite;
        //_reward.CustomizedPlatform -= ChangeSpeedForMobilePlatform;
    }

    private void ChangeSpeedForMobilePlatform(float deceleration)
    {
        _stateMove.ChangeSpeedForMobilePlatform(deceleration);
    }

    private void ChangeState(State state)
    {
        if (_currentState != null)
        {
            _currentState.enabled = false;
        }

        _currentState = state;
        _currentState.enabled = true;
    }

    private void SetStateRestartScene()
    {
        ChangeState(_stateRestartScene);
    }

    private void SetStateDie()
    {
        ChangeState(_stateDie);
        _score.IncreaseScore();
    }

    private void SetStateAttack()
    {
        _isAttack = true;

        if (_currentState != _stateRestart)
        {
            _colliderHandler.enabled = false;
            ChangeState(_stateAttack);
        }
    }

    private void SetStateMove()
    {
        ChangeState(_stateMove);

        if (_isAttack == true)
        {
            SetStateAttack();
        }
        if (_isComplete == true)
        {
            SetStateComplite();
        }
    }

    private void SetStateRestart()
    {
        ChangeState(_stateRestart);
    }

    private void SetStateComplite()
    {
        _isComplete = true;
        _colliderHandler.enabled = false;

        if (_currentState != _stateRestart)
        {
            ChangeState(_stateComplite);
        }
    }
}
