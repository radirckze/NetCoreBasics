# .NET async programming

This folder is my ASP async programming testbed

Note to self ***
There is a lot of misleading / incorrect information on this topic. So, stick to
official / vetted documentation. Some good resources:

https://devblogs.microsoft.com/pfxteam/await-synchronizationcontext-and-console-apps/  (3 parts. Part 1 explains everything you need to know.)
https://blog.stephencleary.com/2012/02/async-and-await.html
https://msdn.microsoft.com/en-us/magazine/dn802603.aspx
https://docs.microsoft.com/en-us/dotnet/csharp/async
[ExecutuionContext and SynchronizationContext] https://devblogs.microsoft.com/pfxteam/executioncontext-vs-synchronizationcontext/
[Best practices] https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
[Deadlock issue] https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html

The concept is fairly simple. The two key concepts are: async modifier and await.

The Async keyword enables the await keyword in the method and changes how the results are handled. 
Await relinquishes (if necessary) execution to the calling method with a task object that can be used to query status, etc.
Note: under the covers, the await call actually create a callback. Essentially, await is masking the need to create a callback.

Note: task.wait() and task.Result are blocking calls. Still useful in some cases.

### Some best practice notes 
Below tables copied directly from https://msdn.microsoft.com/en-us/magazine/jj991977.aspx

Name | Description | Exception
--- | --- | ---
Avoid async void | Prefer async Task methods over async void methods | Event handlers
Async all the way | Don’t mix blocking and async code | Console main method
Configure context | Use ConfigureAwait(false) when you can | Methods that require con­text

To do this | instead of this | use this
--- | --- | ---
Retrieve the result of a background task | Task.Wait or Task.Result | await
Wait for any task to complete | Task.WaitAny | await Task.WhenAny
Retrieve the results of multiple tasks | Task.WaitAll | await Task.WhenAll
Wait a period of time | Thread.Sleep | await Task.Delay

Warning: The code in this folder is for my personal testing. It does not contain
any try catch blocks. Ideally need to check whether task completed, faulted, 
cancelled, etc., and handle those cases. 


