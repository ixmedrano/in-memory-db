namespace DevotedDatabase
{

    public class Deleter
    {
        public Deleter(Database inMemoryDB, string column, Table table)
        {
            // Check if column already exists
            if(table.Row.Find(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber) == null)
            {
                return;
            }
            else
            {
                Row currentRow = table.Row.Find(i => i.Column == column && i.TransactionId == inMemoryDB.TransactionNumber);
                currentRow.Value = "NULL";
            }

        }
    }
}