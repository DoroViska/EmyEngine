using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangTester
{
    public class Test : IDisposable
    {

        public string data;

      
        public Test()
        {
           


        }




        public bool IsDisposed { private set; get; } = false;
        public void Close()
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
            Console.WriteLine("Test::Close");

        }
        ~Test()
        {
            Close();
        }
        public void Dispose()
        {
            Close();
            GC.SuppressFinalize(this);
        }
    }




    class Program
    {
        static void func()
        {
            Test tc = new Test();
            
                tc.data = "asdasd";
            
                
        }

        static void Main(string[] args)
        {

            func();
            GC.Collect();
            Console.ReadKey();



            

        }
    }
}
