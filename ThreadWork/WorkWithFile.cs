using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ThreadWork
{
    public static class WorkWithFile
    {
        public static List<Settings> ReadFile()
        {
            string jsonFile;
            try
            {
                jsonFile = File.ReadAllText("settings.json");
            }
            catch
            {
                var result = new Result()
                {
                    Error = "Not founded file settings.json",
                    Primes = null,
                    Success = false,
                    Duration = null
                };
                LoadResult(result);
                return null;
            }
            try
            {
                JsonSerializer.Deserialize<List<Settings>>(jsonFile);

            }
            catch
            {
                var result = new Result()
                {
                    Error = "Invalid file settings.json",
                    Primes = null,
                    Success = false,
                    Duration = null
                };
                LoadResult(result);
                return null;
            }

            var listOfNum = JsonSerializer.Deserialize<List<Settings>>(jsonFile);

            var noNullList = listOfNum.Select(x => x).Where(x =>x != null);

            return noNullList.ToList();
        }

        public static void CreateResult(Stopwatch timer, List<int> listPrime)
        {
            var result = new Result
            {
                Success = true, Error = null, Duration = timer.Elapsed.ToString(), Primes = listPrime.ToArray()
            };
            LoadResult(result);

        }

        private static void LoadResult(Result resultOfArray)
        {
           
                var result = JsonSerializer.Serialize(resultOfArray);
                File.WriteAllText("Result.json", result);
            
        }
    }
}