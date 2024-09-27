using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    NavMeshAgent _agent;
    public NavMeshAgent Agent => _agent;
    State _currentState;
    
    public Transform startPointA, startPointB;

    void Awake() {
        _agent = GetComponent<NavMeshAgent>();
        _currentState = new Wait(this);
    }

    void Start() {
        _currentState.DoAction();
    }

    void Update() {
        _currentState.CheckDestination();
    }
}
