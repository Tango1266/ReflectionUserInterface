using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoUIConsole.Components
{
    public class Table
    {
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Row> Rows { get; set; } = new List<Row>();

        public static readonly string[] DefaultSeperator =  { ";" };
        public string[] Seperator { get; set; } = DefaultSeperator;

        public void AddColumn(params string[] columnNames)
        {
            foreach (string columnNmae in columnNames)
            {
                if(!Columns.Any(x => x.Name.Equals(columnNmae))) Columns.Add(new Column(this, columnNmae));
                else Helper.Log("Tried to add an existing column.");
            }
        }

        public void AddRow(params string[] rowContent)
        {
            if(rowContent.Length == 1) Rows.Add(new Row(this, rowContent[0]));
            else Rows.Add(new Row(this, rowContent));
        }

        public bool AddItem(Column column, Row row, string content)
        {
            if (!Columns.Contains(column) ) return false;
            var currentRow = row;

            if (!Rows.Contains(row)) Rows.Add(row);
            else currentRow = Rows.Find(x => x.Equals(row));

            currentRow.AddItem(column, content);
            return true;
        }

        public void Draw()
        {
            Columns.ForEach(x => Helper.LogInLine(x.ToString()));
            Helper.Log("\n------------------------");
            Rows.ForEach(x => Helper.Log(x.ToString()));
        }
    }

    public class Row : IEquatable<Row>
    {
        public List<string> Content;
        private Dictionary<Column,string> ContentDict { get; set; } =new Dictionary<Column, string>();

        public int CountItems => Content.Count;
        public Table Table { get; set; }
        public string[] Seperator { get; set; }// = Table.DefaultSeperator;

        public Row(Table table, string content)
        {
            Content = content.Split(table.Seperator, StringSplitOptions.None).ToList();
            InitializeTableInfo(table);
        }

        public Row(Table table, string[] content)
        {
            Content = content.ToList();
            InitializeTableInfo(table);
        }

        public Row(Table table)
        {
            InitializeTableInfo(table);
        }

        private void InitializeTableInfo(Table table)
        {
            List<string> rowContent = Content ?? ContentDict.Values.ToList();
            Seperator = Seperator ?? table.Seperator;
            Table = table;

            if(rowContent.Count <= 0) return;

            //Fill rows in alignment to columns from table
            int colRow = 0;
            foreach (var column in table.Columns)
            {
                if(colRow >= table.Columns.Count) break;

                AddItem(column, rowContent[colRow++]);
            }

            //Extend columns when row content exeeds columns
            for (int i = colRow; i < rowContent.Count; i++)
            {
                AddItem(new Column(table,$"Spalte {i}"), rowContent[colRow++]);
            }
        }

        internal bool AddItem(Column column, string content)
        {
            try
            {
                if (!ContentDict.ContainsKey(column))
                {
                    ContentDict.Add(column, content);
                    return true;
                }

                ContentDict[column] = content;
            }
            catch (Exception e)
            {
                Helper.Log("Zeile konnte nicht befuellt werden", e);
                return false;
            }

            return true;
        }

        #region EquatableMember

        public bool Equals(Row other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Content.SequenceEqual(other.Content);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Row)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Content != null ? Content.GetHashCode() : 0) * 397) ^ (Seperator != null ? Seperator.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            List<string> rowContent = Content ?? ContentDict.Values.ToList();
            var res = string.Empty;
            foreach (var column in rowContent)
            {
                res += column + Seperator[0];
            }
            return res.TrimEnd(char.Parse(Seperator[0]));
        }

        #endregion
    }

    public class Column : IEquatable<Column>
    {
        public Table Table { get; }
        public string Name { get; set; }

        public Column(Table table, string name)
        {
            Table = table;
            Name = name;
        }

        #region EquatableMember

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Column other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Column)obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        #endregion
    }
}
