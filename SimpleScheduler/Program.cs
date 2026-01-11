// Grab the Scheduler instance from the Factory

using Microsoft.Extensions.Logging;
using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using SimpleScheduler;

var loggerFactory = LoggerFactory.Create(builder =>
{
    builder
        .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Debug)
        .AddSimpleConsole(options =>
        {
            options.IncludeScopes = true;
            options.SingleLine = true;
            options.TimestampFormat = "hh:mm:ss ";
        });
});

LogContext.SetCurrentLogProvider(loggerFactory);

StdSchedulerFactory factory = new StdSchedulerFactory();
IScheduler scheduler = await factory.GetScheduler();

// and start it off
await scheduler.Start();

var auction = new Auction { Name = "Sample Auction" };

// define the job and tie it to our RunAuctionJob class
IJobDetail job = JobBuilder.Create<RunAuctionJob>()
    .WithIdentity("auctionJob", "group1")
    .UsingJobData(new JobDataMap { { "auction", auction } })
    .Build();

// Trigger the job to run now
ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .Build();

// Tell Quartz to schedule the job using our trigger
await scheduler.ScheduleJob(job, trigger);

Console.WriteLine($"[Program] Auction scheduled. Initial state: {auction.State}");

// some sleep to show what's happening
await Task.Delay(TimeSpan.FromSeconds(5));
Console.WriteLine($"[Program] Final Auction state: {auction.State}");

// and last shut down the scheduler when you are ready to close your program
await scheduler.Shutdown();