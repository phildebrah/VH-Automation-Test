using System;
using System.Linq;

namespace TestLibrary.Utilities
{
    public class Util
    {
      
        public static string RandomString(int stringLenth = 10)
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, stringLenth)
             .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        public static string RandomAlphabet(int stringLenth = 10)
        {
            Random rnd = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, stringLenth)
             .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
