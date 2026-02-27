using Microsoft.Data.SqlClient;

namespace IntroductionToADO.Introduction
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

		public void Select(string fields, string tables, string condition = "")
		{
			string cmd = $"SELECT {fields} FROM {tables}";
			if (condition != "") cmd += $" WHERE {condition}";
			cmd += ";";
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

    public void Insert(string table, string values)
	  {
			string cmd = $"INSERT INTO {table} VALUES ({values})";
			connection.Open();
			SqlCommand command = new SqlCommand(cmd, connection);
			command.ExecuteNonQuery();
			connection.Close();
		}

    public void Update(string table, string setClause, string condition = ""){
      string cmd = $"UPDATE {table} SET {setClause}";
      if (!string.IsNullOrWhiteSpace(condition)) cmd += $" WHERE {condition}";
      cmd += ";";
      connection.Open();
      SqlCommand command = new SqlCommand(cmd, connection);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public void AddPrimaryKey(string table, string column){
      string cmd = $"ALTER TABLE [{table}] ADD CONSTRAINT PK_{table}_{column} PRIMARY KEY ([{column}]);";
      connection.Open();
      SqlCommand command = new SqlCommand(cmd, connection);
      command.ExecuteNonQuery();
      connection.Close();
    }

    public void AddForeignKey(string table, string column, string refTable, string refColumn)
    {
      string constraintName = $"FK_{table}_{refTable}";
      string cmd = $"ALTER TABLE [{table}] ADD CONSTRAINT {constraintName} FOREIGN KEY ([{column}]) REFERENCES [{refTable}] ([{refColumn}]);";
      connection.Open();
      SqlCommand command = new SqlCommand(cmd, connection);
      command.ExecuteNonQuery();
      connection.Close();
    }
  }
}
