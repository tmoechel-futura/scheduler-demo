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
    private readonly StateMachine<AuctionState, AuctionTrigger> _auctionStateMachine;

    public AuctionStateMachine(AuctionState initialState = AuctionState.InPreparation)
    {
        _auctionStateMachine = new StateMachine<AuctionState, AuctionTrigger>(initialState);

        _auctionStateMachine.Configure(AuctionState.InPreparation)
            .Permit(AuctionTrigger.Run, AuctionState.Running);
    }

    public AuctionState State => _auctionStateMachine.State;

    public void Run() => _auctionStateMachine.Fire(AuctionTrigger.Run);
}