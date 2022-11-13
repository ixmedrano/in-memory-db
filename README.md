# in-memory-db
in memory database assignment

This project was created for Devoted Health as a part of the technical assesment.

This is a console application written in C# for .NET 7. There are no third party libraries, all of the functionality exists within the standard .NET deployment from Microsoft.

The application functions as an in memory database where the data is wiped out at the end of the session. The application makes use of classes and objects to hold and update data based on user input. The application can also handle transaction arguements through the use of instanced objects and temporary table storage. When a commin argument is issued the application merges the indicated transactions. The application can handle multiple layers of transactions and keeps track of what updates were dont on which transaction layers. When a transaction is commited the data in that layer is cleared and the memory is freed up.

The project was challenging when designing a system that could handle nested transactions. I had to redesign pieces of the code when i got to the final test case as my original design began to throw some errors when attempting to update multiple transactions at a time.

