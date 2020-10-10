using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace thread_test
{
	public class Program
	{
		static TcpIpServer server = null; 

		public static void Main( string[] args )
		{
			server = new TcpIpServer();
			CreateHostBuilder( args ).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder( string[] args ) =>
			Host.CreateDefaultBuilder( args )
				.ConfigureWebHostDefaults( webBuilder =>
				 {
					 webBuilder.UseStartup<Startup>();
				 } );
	}
}
