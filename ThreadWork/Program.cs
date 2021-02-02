using System.Diagnostics;

namespace ThreadWork
{
    class Program
    {
        static void Main(string[] args)
        {

            var time = new Stopwatch();
            time.Start();

            var listOfNum = WorkWithFile.ReadFile();
            if (listOfNum == null)
            {
                return;
            }
            var threadWork = new ThreadSafeHashSet();

            threadWork.CreateThread(listOfNum);
            time.Stop();

            WorkWithFile.CreateResult(time, threadWork.GetList());


        }


    }
}