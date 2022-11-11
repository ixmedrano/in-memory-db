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
            
            Console.WriteLine("File Path: {0}", incomingFilePath);
            Database inMemoryDB = new Database();
            inMemoryDB.DatabaseName = "DevotedData";
            inMemoryDB.Table = new List<Table>();

            var lines = File.ReadLines(incomingFilePath);
            int lineIndex = 1;
            foreach (string line in lines)
            {
                lineIndex ++;
                Console.WriteLine(line);
                string commandType = line.Split(":")[0];
                
                switch (commandType)
                {
                    case "SET":
                    string setCommandName = line.Split(":")[1];
                    string setCommandValue = line.Split(":")[2];
                    Setter setter = new Setter(inMemoryDB,setCommandName,setCommandValue, lineIndex);
                    break;
                    case "GET":
                    string getCommandName = line.Split(":")[1];
                    Console.WriteLine("This is a GET.");
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