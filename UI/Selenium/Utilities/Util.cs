using System;
using System.Linq;

namespace TestLibrary.Utilities
{
    public class Util
    {
      
        public static string RandomString()
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
             .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
