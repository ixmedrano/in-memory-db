using System.Diagnostics;

namespace DevotedDatabase
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Database Starting");
            // TODO convert to STDIN input
            string incomingFilePath = @"F:\Storage\TestingFiles\incoming\sample_input.txt";
            // Variables
            string databaseName = "DevotedDataBase";
            string tableName = "DevotedDataTable";
            // Instantiate Database Object
            Database inMemoryDB = new Instantiator(databaseName, tableName).CreateDatabase();
            // Locate existing table
            Table table = inMemoryDB.Table.Find(i => i.TableName == tableName);
            string delimiter = " ";

            var lines = File.ReadLines(incomingFilePath);
            int lineIndex = 1;
            foreach (string line in lines)
            {
                lineIndex ++;
                Console.WriteLine(line);
                string commandType = line.Split(" ")[0];
                string commandName = line.Split(delimiter)[1];
                switch (commandType)
                {
                    case "SET":
                    string setCommandValue = line.Split(delimiter)[2];
                    Setter setter = new Setter(inMemoryDB, commandName, setCommandValue, lineIndex, table);
                    break;
                    case "GET":
                    Getter getter = new Getter(inMemoryDB, commandName, lineIndex, table);                 
                    break;
                    case "DELETE":
                    Deleter deleter = new Deleter(inMemoryDB, commandName, lineIndex, table);
                    break;
                    case "COUNT":
                    // Known as value in the test case but can re-use commandName variable as input
                    Counter counter = new Counter(inMemoryDB, commandName, lineIndex, table);
                    break;
                    default: Console.WriteLine("Unrecognized Command");
                    break;
                }
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
            string rowName = "DevotedDataRow";
            Row newRow = new Row();
            newRow.Column = rowName;
            newRow.RowId = Guid.NewGuid();
            newRow.DateCreated = DateTime.Today;
            newRow.DateUpdated = DateTime.Today;
            // Add Row Object to Table Object
            newTable.Row.Add(newRow);
*/