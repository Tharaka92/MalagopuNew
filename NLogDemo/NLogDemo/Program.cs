using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogDemo
{
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                Console.Out.WriteLine("Logging Demo Started.");
                Console.Out.WriteLine("---------------------");
                Console.Out.WriteLine("");

                throw new DivideByZeroException();
            }
            catch (DivideByZeroException ex)
            {
                Console.Out.WriteLine("Exception thrown and caught. Logging started");

                logger.ErrorException("Exception Occured", ex);

                Console.Out.WriteLine("Exception logged successfully. You can find the log file at C:/log/nLogDemo.log.");
                Console.Out.WriteLine("");

                Console.Out.WriteLine("Hit any key to exit");
                Console.ReadKey();
            }
        }
    }
}
