# .NET async programming

This folder is my ASP async programming testbed

Note to self ***
There is a lot of misleading / incorrect information on this topic. So, stick to
official / vetted documentation. Some good resources:

https://docs.microsoft.com/en-us/dotnet/csharp/async
(Check out the Tasked Based Asynchronous Pattern or TAP link)

https://msdn.microsoft.com/en-us/magazine/dn802603.aspx

https://www.microsoft.com/en-us/download/details.aspx?id=19957 

The concept is fairly simple. The two key concepts are: async modifier and await.

Await relinquishes (if necessary) execution to the calling method with a task object that can be used to query status, etc.

Note: task.wait() is a blocking call. Still useful in some cases.

Note: under the covers, the await call actually create a callback. Essentially, await is masking the need to create a callback.

Warning: The code in this folder is for my personal testing. It does not contain
any try catch blocks. Ideally need to check whether task completed, faulted, 
cancelled, etc., and handle those cases. 

The calls in Program.cs are not all async. For a console application, this is OK as the console application is not a service, is single threaded by default. But for an ASP service (WebAPI method for example) we should use async methods with await throughout the chain so the thread gets released whenever there is async work going on allowing other calls to the server to execute.

