using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    State _currentState;
    
    public Transform startPointA, startPointB;
    
    [SerializeField, Range(1,5)] float waitDuration = 3.0f;

    void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = new Wait(this, waitDuration);
    }

    void Start() {
        _currentState.DoAction();
    }

    void Update() {
        _currentState.CheckDestination();
    }

    public void ChangeState(State newState) {
        _currentState = newState;
    }
}
