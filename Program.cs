using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace IntroductionToADO.Introduction
{
	class Program
	{
		static void Main(string[] args)
		{
			string connection_string = "Server=.;Initial Catalog=Movies_SPU_411;TrustServerCertificate=True;User Id=sa;Password=Password1;";
			SqlConnection connection = new SqlConnection(connection_string);
			string cmd = "SELECT title,year,first_name,last_name FROM Movies JOIN Directors ON(director=director_id)";
			
      Connector connector = new Connector(connection_string);

			connector.Select("title,year,first_name,last_name","Movies,Directors","director=director_id");

			Console.WriteLine("\n-------------------------------------------------------------\n");
      // string table = "Directors";
			// connector.Insert("Directors", "6,N'Tarantino',N'Quentin'");
      // connector.Select(cmd);
      // Console.WriteLine(connector.Scalar($"SELECT MAX({connector.GetNextPrimaryKey("Directors")} + 1) FROM Directors"));
      // Console.WriteLine(connector.GetPrimaryKeyColumn(table));
      // Console.WriteLine(connector.GetNextPrimaryKey(table));
      // Console.WriteLine(connector.GetLastPrimaryKey(table));
      // connector.Insert("Directors","6, N'Besson', N'Luc'");

			Console.WriteLine("\n-------------------------------------------------------------\n");


			connector.Select("*", "Directors");

      Console.WriteLine(connector.GetPrimaryKey("SELECT director_id FROM Directors WHERE last_name=N'Cameron' AND first_name=N'James'"));
      Console.WriteLine(connector.GetPrimaryKey("Directors", "last_name, first_name", "Cameron, James"));
     Console.WriteLine(connector.GetPrimaryKeyColumn("Movies"));
      Console.WriteLine(connector.GetPrimaryKey("Movies", "title, year", "The Heat, 1995-12-15"));
		}
	}
}
