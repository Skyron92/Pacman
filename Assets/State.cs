using UnityEngine;

public abstract class State : MonoBehaviour
{
    Ghost _owner;
    protected Ghost Owner => _owner;
    protected Vector3 Destination;
    protected bool HasReachedDestination => Vector3.Distance(Owner.transform.position, Destination) < 0.42f;

    protected State(Ghost owner) {
        _owner = owner;
    }

    public abstract void DoAction();
    public abstract void CheckDestination();
    public abstract void TryChangeState();
}

public class Wait : State{
    
    public Wait(Ghost ghost) : base(ghost) {
        Destination = Owner.startPointA.position;
    }

    public override void DoAction() {
        Owner.Agent.SetDestination(Destination);
    }

    public override void CheckDestination() {
        if(HasReachedDestination) {
            if(Destination == Owner.startPointA.position) {
                Destination = Owner.startPointB.position;
            }
            else Destination = Owner.startPointA.position; 
            DoAction();
        }
    }

    public override void TryChangeState() {
        throw new System.NotImplementedException();
    }
}
