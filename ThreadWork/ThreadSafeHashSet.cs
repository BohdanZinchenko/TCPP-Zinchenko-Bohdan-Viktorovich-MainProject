using System.Collections.Generic;
using System.Threading;

namespace ThreadWork
{
    public  class ThreadSafeHashSet
    {
      
        private static readonly object Locker = new object();
        private  static readonly List<int> ListPrime = new List<int>();
        private static CountdownEvent _cdEvent;
        public  void CreateThread(List<Settings> settings)
        {
             _cdEvent = new CountdownEvent(settings.Count);

            foreach (var item in settings)
            {
                var newThread = new Thread(()=> PrimeNumbersFind(item.PrimesFrom,item.PrimesTo));
                newThread.Start();
            }

            _cdEvent.Wait();


        }

        public List<int> GetList()
        {
            lock (Locker)
            {
                return ListPrime;
            }
        }
        public void PrimeNumbersFind(int low, int top)
        {
            var listOfNumbers = new List<int>();
                for (var i = low; i < top; i++)
                {
                    if (i <= 1)
                    {
                        continue;
                    }

                    var isPrime = true;
                    for (var j = 2; j < i; j++)
                    {
                        if (i % j == 0)
                        {
                            isPrime = false;
                            break;
                        }
                    }

                    if (!isPrime)
                    {
                        continue;
                    }

                    listOfNumbers.Add(i);
                }
                lock (Locker)
                {
                    foreach (var item in listOfNumbers)
                    {
                        if (ListPrime.Contains(item))
                        {
                            continue;
                        }
                        ListPrime.Add(item);
                    }
                    _cdEvent.Signal();
            }
                

        }
    }
}