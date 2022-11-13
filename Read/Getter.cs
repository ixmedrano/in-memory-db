namespace DevotedDatabase
{
    public class Getter
    {
        public Getter(Database inMemoryDB, string column, Table table)
        {   
            // If the get cannot find the column specified return null
            if (table.Row == null || table.Row.Find(i => i.Column == column) == null)
            {
                Console.WriteLine("NULL");
            }
            // If getter is able to locate a record by the column and transaction id, return that specific value
            else if (table.Row.Find(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber) != null)
            {
                Row currentRow = table.Row.Find(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber);
                Console.WriteLine("{0}",currentRow.Value);
            }
            // Otherwise if it is able to find a column, but not the transaction id, get the latest record located
            else
            {
                Row currentRow = table.Row.FindLast(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber);
                Console.WriteLine("{0}",currentRow.Value);
            }
        }
    }
}