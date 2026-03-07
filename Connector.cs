using Microsoft.Data.SqlClient;

namespace IntroductionToADO
{
	class Connector
	{
		string connection_string;
		SqlConnection connection;
		public Connector(string connection_string)
		{
			this.connection_string = connection_string;
			this.connection = new SqlConnection(connection_string);
		}
    public void Execute(string cmd){
    
    }
		public void Select(string cmd)
		{
			connection.Open();
			SqlCommand command = new SqlCommand(cmd, connection);
			SqlDataReader reader = command.ExecuteReader();
			while (reader.Read())
			{
				//Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}");
				for (int i = 0; i < reader.FieldCount; i++)
				{
					Console.Write(reader[i].ToString().PadRight(29));
				}
				Console.WriteLine();
			}
			reader.Close();
			connection.Close();
		}
		public void Select(string fields, string tables, string condition = "")
		{
			string cmd = $"SELECT {fields} FROM {tables}";
			if (condition != "") cmd += $" WHERE {condition}";
			cmd += ";";
			Select(cmd);
		}
		public void Insert(string cmd)
		{
			connection.Open();
			SqlCommand command = new SqlCommand(cmd, connection);
			command.ExecuteNonQuery();
			connection.Close();
		}
		public void Insert(string table, string values)
		{
			string cmd = $"INSERT INTO {table} VALUES ({values})";
			Insert(cmd);
		}
		public object Scalar(string cmd)
		{
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
		object value = command.ExecuteScalar();
			//int value = Convert.ToInt32(command.ExecuteScalar());
			connection.Close();
			return value;
		}
    public object GetPrimaryKey(string cmd){
      SqlCommand command = new SqlCommand(cmd,connection);
      connection.Open();
      object primaryKey = command.ExecuteScalar();
      connection.Close();
      return primaryKey;
    }
    public object GetPrimaryKey(string table, string fields, string values){
      string[] s_fields = fields.Split(',');
      string[] s_values = values.Split(',');
      if(s_fields.Length != s_values.Length) return null;
      string condition = "";
      for(int i = 0; i < s_values.Length;i++){
        condition += $"{s_fields[i].Trim()}=N'{s_values[i].Trim()}'";
        if(i != s_values.Length - 1) condition += " AND ";
      }
      string cmd = $"SELECT {GetPrimaryKeyColumn(table)} FROM {table} WHERE {condition}";
      return Scalar(cmd);
    }
		public string GetPrimaryKeyColumn(string table)
		{
string cmd = $"SELECT COLUMN_NAME " +
$"FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE " +
$"WHERE CONSTRAINT_NAME = (SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_NAME = N'{table}' AND CONSTRAINT_TYPE=N'PRIMARY KEY')";
			return (string)Scalar(cmd);
// 			return (string)Scalar
// (
// $"SELECT COLUMN_NAME " +
// $"FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE " +
// $"WHERE CONSTRAINT_NAME = (SELECT CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_NAME = N'{table}' AND CONSTRAINT_TYPE=N'PRIMARY KEY')"
// );
		}
		public int GetLastPrimaryKey(string table)
		{
			return Convert.ToInt32(Scalar($"SELECT MAX({GetPrimaryKeyColumn(table)}) FROM {table}"));
		}
		public int GetNextPrimaryKey(string table)
		{
			return GetLastPrimaryKey(table) + 1;
		}

		public void Update(string cmd)
		{
			SqlCommand command = new SqlCommand(cmd, connection);
			connection.Open();
			command.ExecuteNonQuery();
			connection.Close();
		}
	}
}
