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

			// connector.Insert("Directors", "6,N'Tarantino',N'Quentin'");
			// connector.Select("*", "Directors");
		}
	}
}
