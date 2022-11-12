namespace DevotedDatabase
{

    public class Counter
    {
        public Counter(Database inMemoryDB, string value, int lineIndex, Table table)
        {
            // Check if column already exists
            if(table.Row.Find(i => i.Value == value) == null)
            {
                Console.WriteLine("0");
            }
            else
            {
                List<Row> currentRow = table.Row.FindAll(i => i.Value == value);
                Console.WriteLine(currentRow.Count);
            }

        }
    }
}