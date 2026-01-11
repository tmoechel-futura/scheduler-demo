using Stateless;

namespace SimpleScheduler;

public enum AuctionState
{
    InPreparation,
    Running
}

public enum AuctionTrigger
{
    Run
}

public class AuctionStateMachine
{
    private readonly StateMachine<AuctionState, AuctionTrigger> _machine;

    public AuctionStateMachine(AuctionState initialState = AuctionState.InPreparation)
    {
        _machine = new StateMachine<AuctionState, AuctionTrigger>(initialState);

        _machine.Configure(AuctionState.InPreparation)
            .Permit(AuctionTrigger.Run, AuctionState.Running);
    }

    public AuctionState State => _machine.State;

    public void Run() => _machine.Fire(AuctionTrigger.Run);
}