# in-memory-db
in memory database assignment

This project was created for Devoted Health as a part of the technical assesment.

# Overview of technology used
This is a console application written in C# for .NET 7. There are no third party libraries, all of the functionality exists within the standard .NET deployment from Microsoft.

# Summary
The application functions as an in memory database where the data is wiped out at the end of the session. The application makes use of classes and objects to hold and update data based on user input. The application can also handle transaction arguements through the use of instanced objects and temporary table storage. When a commit argument is issued the application merges the indicated transactions. The application can handle multiple layers of transactions and keeps track of what updates were done on which transaction layers. When a transaction is commited the data in that layer is cleared and the memory is freed up.

# Challenges
The project was challenging when designing a system that could handle nested transactions. I had to redesign pieces of the code when i got to the final test case as my original design began to throw some errors when attempting to update multiple transactions at a time.

# Future updates
Further user testing is required to ensure all business cases are met but for now all test cases provided are passing.

# How to run the application
To run the application, make sure you have .net 7 installed on the environment your using. Then go to the github link provided and download the project. Once you have the project on your local machine go to "...\in-memory-db\bin\Debug\net7.0" and use the "in-memory-db.exe" to start a session.