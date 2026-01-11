namespace SimpleScheduler;

public class Auction
{
    public string Name { get; set; } = string.Empty;
    public AuctionState State { get; set; } = AuctionState.InPreparation;
}