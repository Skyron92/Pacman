using UnityEngine;

public abstract class State
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

public class Wait : State {

    private float _duration;
    private float _timer;
    
    public Wait(Ghost ghost, float duration) : base(ghost) {
        Destination = Owner.startPointA.position;
        _duration = duration;
        _timer = 0;
    }

    public override void DoAction() {
        Owner.Agent.SetDestination(Destination);
    }

    public override void CheckDestination() {
        _timer += Time.deltaTime;
        if (_timer >= _duration) {
            TryChangeState();
            return;
        }
        if(HasReachedDestination) {
            if(Destination == Owner.startPointA.position) {
                Destination = Owner.startPointB.position;
            }
            else Destination = Owner.startPointA.position; 
            DoAction();
        }
    }

    public override void TryChangeState() {
        Owner.ChangeState(new Hunt(Owner));
    }
}

public class Hunt : State {
    public Hunt(Ghost owner) : base(owner) { }
    public override void DoAction() {
        throw new System.NotImplementedException();
    }

    public override void CheckDestination() {
        throw new System.NotImplementedException();
    }

    public override void TryChangeState() {
        throw new System.NotImplementedException();
    }
}
