namespace DevotedDatabase
{
    public class Row
    {
        public Guid  RowId {get;set;}
        public string  Column {get;set;}
        public string ? Value {get;set;}
        public DateTime  DateCreated {get;set;}
        public DateTime  DateUpdated {get;set;}
    }
    public class Table
    {
        public string TableName {get;set;}
        public List<Row> ? Row {get;set;}   
    }
    public class Database
    {
        public string DatabaseName {get;set;}
        public List<Table> ? Table {get;set;}
    }

}