using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace thread_test
{
	public class TcpIpServer
	{
		Thread t = new Thread( new ThreadStart( ThreadProc ) );

		public TcpIpServer()
		{
			t.Start();
		}

		public static void ThreadProc()
		{
			int i = 0;
			while( true )
			{
				Console.WriteLine( "ThreadProc: {0}", i++ );
				Thread.Sleep( 1000 );
			}
		}
	}
}
