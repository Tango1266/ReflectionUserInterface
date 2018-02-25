using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoUIConsole.Components
{
    public class Table
    {
        public List<Column> Columns { get; set; } = new List<Column>();
        public List<Row> Rows { get; set; } = new List<Row>();

        public void AddColumn(params string[] columnNames)
        {
            foreach (string columnNmae in columnNames)
            {
                Columns.Add(new Column(columnNmae));
            }
        }

        public void AddRow(params string[] rowContent)
        {
            Rows.Add(new Row(rowContent));
        }

        public bool AddItem(Column column, Row row, string content)
        {
            if (!Columns.Contains(column) || !Rows.Contains(row)) return false;

            int columnIndex = Columns.IndexOf(column);
            int rowIndex = Rows.IndexOf(row);


            Rows[rowIndex] = row;

            return true;
        }
    }

    public class Row : IEquatable<Row>
    {
        public List<string> Content;
        public int CountItems => Content.Count;

        public string[] Seperator { get; set; } = { ";" };

        public Row(string content)
        {
            Content = content.Split(Seperator, StringSplitOptions.None).ToList();
        }

        public Row(string[] content)
        {
            Content = content.ToList();
        }

        public void AddItem(string content, int pos = -1)
        {
            if (pos.Equals(-1))
                Content.Add(content);
            else
            {
                if (pos >= CountItems) ExtendRow(pos - CountItems);
                Content.Insert(pos, content);
            }
        }

        private void ExtendRow(int desiredExtension)
        {
            for (int j = 0; j < desiredExtension; j++)
            {
                Content.Add(string.Empty);
            }
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

        #endregion
    }

    public class Column : IEquatable<Column>
    {
        public string Name { get; set; }

        public Column(string name)
        {
            Name = name;
        }

        #region EquatableMember

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
