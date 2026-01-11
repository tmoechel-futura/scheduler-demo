using Quartz;

namespace SimpleScheduler;

public class RunAuctionJob : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var dataMap = context.JobDetail.JobDataMap;
        var auction = (Auction)dataMap["auction"];

        Console.WriteLine($"[RunAuctionJob] Starting auction: {auction.Name}. Current state: {auction.State}");

        var stateMachine = new AuctionStateMachine(auction.State);
        stateMachine.Run();
        auction.State = stateMachine.State;

        Console.WriteLine($"[RunAuctionJob] Auction: {auction.Name} is now in state: {auction.State}");

        await Task.CompletedTask;
    }
}
