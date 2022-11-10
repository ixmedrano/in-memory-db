namespace DevotedDatabase
{
    public class Row
    {
        public Guid RowId {get;set;}
        public Dictionary<string,string> RecordValues {get;set;}
        public DateTime DateCreated {get;set;}
        public DateTime DateUpdated {get;set;}
    }
    public class Table
    {
        public string TableName {get;set;}
        public Row Row {get;set;}   
    }
    public class Database
    {
        public string DatabaseName {get;set;}
        public Table Table {get;set;}
    }

}