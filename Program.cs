using System.Diagnostics;

namespace DevotedDatabase
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database Starting");
            // Variables
            string databaseName = "DevotedDataBase";
            string tableName = "DevotedDataTable";
            string tempTableName = String.Concat(tableName, "Temporary");
            string delimiter = " ";
            bool activeIndicator = true;
            // Instantiate Database Object
            Database inMemoryDB = new Instantiator(databaseName, tableName, tempTableName).CreateDatabase();
            Console.WriteLine("Enter a command:");
            while (activeIndicator)
            {
                // Read from console
                string line = Console.ReadLine();
                // Set transaction indicator to false until a transaction command is given
                bool transactionIndicator = false;
                // If the comman END is given, exit the loop and close the application
                if (line.Split(" ")[0].ToUpper() == "END")
                {
                    Console.WriteLine("Exiting Database");
                    activeIndicator = false;
                    break;
                }
                // This holds the main logic of the application
                TransactionHandler(line, inMemoryDB, tableName, tempTableName, delimiter, transactionIndicator);
            }
        }
        static void TransactionHandler(string line, Database inMemoryDB, string tableName, string tempTableName
                                       , string delimiter, bool transactionIndicator)
        {
            // Locate and refresh tables
            Table livetable = inMemoryDB.Table.Find(i => i.TableName == tableName);
            Table tempTable = inMemoryDB.Table.Find(i => i.TableName == tempTableName);
            // Cleanse command string
            string commandType = line.Split(" ")[0].ToUpper();
            // Update indicator if transaction has started
            if (commandType == "BEGIN")
            {
                // Increment Transaction ID / Number when new transaction is started
                inMemoryDB.TransactionNumber++;
                // If at base transaction, simply merge tables, this will update transaction with latest live data
                if (inMemoryDB.TransactionNumber == 1)
                {
                    MergeTables(inMemoryDB, livetable, tempTable);
                }
                // If several transactions deep, use find to locate relevant transactions and update primary transaction record
                else
                {
                    List<Row> currentTranRows = inMemoryDB.Table.Find(i => i.TableName == tempTableName)
                                                           .Row.FindAll(b => b.TransactionId == inMemoryDB.TransactionNumber - 1);
                    Table currentTranTable = new Table();
                    currentTranTable.Row = new List<Row>(currentTranRows);
                    MergeTables(inMemoryDB, currentTranTable, tempTable);
                }             
                Console.WriteLine("Transaction Started");
                return;
            }
            // For roll back command empty object and instantiate a new object
            if (commandType == "ROLLBACK")
            {
                // Empty transactions based on which transaction id is being rolled back
                tempTable.Row.RemoveAll(i => i.TransactionId == inMemoryDB.TransactionNumber);
                // Decrement transaction number when roll back is issued
                inMemoryDB.TransactionNumber--;
                // Handle user error, rolling back too many times
                if (inMemoryDB.TransactionNumber < 0)
                {
                    inMemoryDB.TransactionNumber = 0;
                }
                Console.WriteLine("Rollback completed");
                return;
            }
            // Commit command to ensure transactions are saved
            if (commandType == "COMMIT")
            {
                // Decrement transaction number
                inMemoryDB.TransactionNumber --;
                // If at base transaction simply merge tables, this avoids enumeration error. C# doesnt like objects being updated while they are being used
                if(inMemoryDB.TransactionNumber == 0)
                {
                    MergeTables(inMemoryDB, tempTable, livetable);
                    tempTable.Row.Clear();
                }
                // If several transactions deep, use setter to update transactions
                foreach(Row row in tempTable.Row)
                {
                    Setter setter = new Setter(inMemoryDB, row.Column, row.Value, tempTable);
                }               
                Console.WriteLine("Transaction Completed");
                return;
            }
            // If its not a transaction use live table for updates and gets
            if (inMemoryDB.TransactionNumber == 0)
            {
                string commandName = line.Split(delimiter)[1];
                Crud(commandType, line, delimiter, inMemoryDB, commandName, livetable);
                return;
            }
            // If it is a transaction use the temporary storage table to do update and gets
            if (inMemoryDB.TransactionNumber != 0)
            {
                string commandName = line.Split(delimiter)[1];
                Crud(commandType, line, delimiter, inMemoryDB, commandName, tempTable);
                return;
            }
        }
        // CRUD holds all of the logic for the database interactions like updates and gets
        static void Crud(string commandType, string line, string delimiter, Database inMemoryDB, string commandName
                           , Table table)
        {
            switch (commandType)
            {
                // If the command is set, use the setter to update or create records
                case "SET":
                    string setCommandValue = line.Split(delimiter)[2];
                    Console.WriteLine("Setting {0} to {1}", commandName, setCommandValue);
                    Setter setter = new Setter(inMemoryDB, commandName, setCommandValue, table);
                    break;
                // If the command is get, use getter to query the specified table
                case "GET":
                    Console.WriteLine("Getting {0}", commandName);
                    Getter getter = new Getter(inMemoryDB, commandName, table);
                    break;
                // If command is delete, use deleter to remove records on the specified table
                case "DELETE":
                    Console.WriteLine("Deleting {0}", commandName);
                    Deleter deleter = new Deleter(inMemoryDB, commandName, table);
                    break;
                //If the command is count, use the counter to get the total number of values
                case "COUNT":
                    // Known as value in the test case but can re-use commandName variable as input
                    Console.WriteLine("Counting {0}", commandName);
                    Counter counter = new Counter(inMemoryDB, commandName, table);
                    break;
                // If the command made it this far, return an unrecognized command wartning to the user
                default:
                    Console.WriteLine("Unrecognized Command");
                    break;
            }
        }
        // This function merges two tables using the setter function
        static void MergeTables(Database inMemoryDB, Table source, Table target)
        {
            List<Row> rowsInSource = source.Row;
            foreach (Row row in rowsInSource)
            {
                Setter mergeTables = new Setter(inMemoryDB, row.Column, row.Value, target, inMemoryDB.TransactionNumber);
            }

        }
    }

}