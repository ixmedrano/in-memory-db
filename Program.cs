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
            Database inMemoryDB = new Instantiator(databaseName, tableName).CreateDatabase();
            Console.WriteLine("Enter a command:");
            string incomingFilePath = @"F:\Storage\TestingFiles\incoming\sample_input.txt";
            var lines = File.ReadLines(incomingFilePath);
            foreach (string line in lines)
            {
                string cleanedLine = line.ToUpper();
                bool transactionIndicator = false;
                TransactionHandler(cleanedLine, inMemoryDB, tableName, tempTableName, delimiter, transactionIndicator);
            }
/*
            while (activeIndicator)
            {
                string line = Console.ReadLine();
                string cleanedLine = line.ToUpper();
                bool transactionIndicator = false;
                if (cleanedLine.Split(" ")[0] == "END")
                {
                    Console.WriteLine("Exiting Database");
                    activeIndicator = false;
                    break;
                }
                TransactionHandler(cleanedLine, inMemoryDB, tableName, tempTableName, delimiter, transactionIndicator);
            }
*/
        }
        static void TransactionHandler(string line, Database inMemoryDB, string tableName, string tempTableName
                                       , string delimiter, bool transactionIndicator)
        {
            // Locate tables
            Table livetable = inMemoryDB.Table.Find(i => i.TableName == tableName);
            Table tempTable = inMemoryDB.Table.Find(i => i.TableName == tempTableName);
            // Local Variables
            string commandType = line.Split(" ")[0];
            // Update indicator if transaction has started
            if (commandType == "BEGIN")
            {
                inMemoryDB.TransactionNumber++;
                if (inMemoryDB.TransactionNumber == 1)
                {
                    MergeTables(inMemoryDB, livetable, tempTable);
                }
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
                tempTable.Row.RemoveAll(i => i.TransactionId == inMemoryDB.TransactionNumber);
                
                inMemoryDB.TransactionNumber--;
                if (inMemoryDB.TransactionNumber < 0)
                {
                    inMemoryDB.TransactionNumber = 0;
                }
                Console.WriteLine("Rollback completed");
                return;

            }
            if (commandType == "COMMIT")
            {
                //TODO: Fix Enumeration issue
                inMemoryDB.TransactionNumber --;
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
            if (inMemoryDB.TransactionNumber != 0)
            {
                string commandName = line.Split(delimiter)[1];
                Crud(commandType, line, delimiter, inMemoryDB, commandName, tempTable);
                return;
            }

        }
        static void Crud(string commandType, string line, string delimiter, Database inMemoryDB, string commandName
                           , Table table)
        {
            switch (commandType)
            {
                case "SET":
                    string setCommandValue = line.Split(delimiter)[2];
                    Setter setter = new Setter(inMemoryDB, commandName, setCommandValue, table);
                    Console.WriteLine("Setting {0} to {1}", commandName, setCommandValue);
                    break;
                case "GET":
                    Getter getter = new Getter(inMemoryDB, commandName, table);
                    break;
                case "DELETE":
                    Deleter deleter = new Deleter(inMemoryDB, commandName, table);
                    Console.WriteLine("Deleting {0}", commandName);
                    break;
                case "COUNT":
                    // Known as value in the test case but can re-use commandName variable as input
                    Counter counter = new Counter(inMemoryDB, commandName, table);
                    break;
                default:
                    Console.WriteLine("Unrecognized Command");
                    break;
            }

        }
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
/*
                string fileName = Path.GetFileName(entry);
                string rejectedFile = System.IO.Path.Combine(rejectedFilePath, fileName);
                string ext = Path.GetExtension(entry);
                string[] fileEntries = Directory.GetFiles(incomingFilePath);
                string rejectedFilePath = @"F:\Storage\TestingFiles\rejected";
*/

/*
                if(inMemoryDB.TransactionNumber == 1)
                {
                    MergeTables(inMemoryDB, tempTable, livetable);
                    tempTable.Row.Clear();
                }
*/
/*          
            string rowName = "DevotedDataRow";
            Row newRow = new Row();
            newRow.Column = rowName;
            newRow.RowId = Guid.NewGuid();
            newRow.DateCreated = DateTime.Today;
            newRow.DateUpdated = DateTime.Today;
            // Add Row Object to Table Object
            newTable.Row.Add(newRow);
*/