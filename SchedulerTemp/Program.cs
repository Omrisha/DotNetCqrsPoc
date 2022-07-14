using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using SchedulerTemp;

LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

var factory = new StdSchedulerFactory();
IScheduler scheduler = await factory.GetScheduler();

await scheduler.Start();

IJobDetail job = JobBuilder.Create<HelloJob>()
    .WithIdentity("job1", "group1")
    .Build();

ITrigger trigger = TriggerBuilder.Create()
    .WithIdentity("trigger1", "group1")
    .StartNow()
    .WithSimpleSchedule(x => x
        .WithIntervalInSeconds(10)
        .RepeatForever())
    .Build();

await scheduler.ScheduleJob(job, trigger);

await Task.Delay(TimeSpan.FromSeconds(10));

await scheduler.Shutdown();