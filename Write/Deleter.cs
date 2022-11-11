namespace DevotedDatabase
{

    public class Deleter
    {
        public Deleter(Database inMemoryDB, string column, int lineIndex, Table table)
        {
            // Check if column already exists, if not create it, else update it
            if(table.Row.Find(i => i.Column == column) == null)
            {
                return;
            }
            else
            {
                Row currentRow = table.Row.Find(i => i.Column == column);
                currentRow.Value = "NULL";
            }

        }
    }
}