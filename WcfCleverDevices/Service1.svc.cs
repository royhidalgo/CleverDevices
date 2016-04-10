using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService3.DAL;
using Dapper;
using System.Data;

namespace WcfCleverDevices
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        ISqlConnection _sql;
        IDbConnection _conn;

        public Service1(ISqlConnection sql)
        {
            _sql = sql;
            _conn = sql.Connect();
        }

        /// <summary>
        /// Return the sentence where words are reversed using C#
        /// </summary>
        /// <param name="sentence"> a sentence consists of words</param>
        /// <returns>Reverse words of the sentence. Return also the input parameter</returns>
        public Reverse ReverseWordsBySql(string sentence)
        {
            var reverse = _conn.Query<Reverse>("SELECT dbo.fn_ReverseWords(@sentence) AS ReverseWords", new { sentence = sentence }).FirstOrDefault();
            reverse.Sentence = sentence;
            return reverse;
        }

        /// <summary>
        /// Return counts of prime number in the range of [1-N]
        /// </summary>
        /// <param name="range">long number, value of N in [1-N]</param>
        /// <returns>Counts of prime numbers from [1-N]</returns>
        public int CountPrimeBySql(long topRange)
        {
            var count = _conn.Query<int>("GetPrimeCount", new { topRange = topRange }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            return count;
        }

        /// <summary>
        /// Return the common characters in a and b using C#
        /// </summary>
        /// <param name="a">the first character string</param>
        /// <param name="b">the second character string</param>
        /// <returns>String of characters common to the two input. Return also the input parameters</returns>
        public CommonCharacters GetCommonCharacterBySql(string a, string b)
        {
            var common = _conn.Query<CommonCharacters>("SELECT dbo.fn_CommonCharacters(@a, @b) AS Common", new { a = a, b = b }).FirstOrDefault();
            common.FirstString = a;
            common.SecondString = b;
            return common;
        }

        /// <summary>
        /// Return the common characters in a and b using C#
        /// </summary>
        /// <param name="a">the first character string</param>
        /// <param name="b">the second character string</param>
        /// <returns>String of characters common to the two input. Return also the input parameters</returns>
        public CommonCharacters GetCommonCharacterByCSharp(string a, string b)
        {
            CommonCharacters common = new CommonCharacters();
            string ret = string.Empty;

            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < b.Length; j++)
                {
                    if (a[i] == b[j])
                    {
                        if (ret.IndexOf(a[i]) == -1 && a[i].ToString() != " ")
                            ret += a[i];
                        break;
                    }
                }
            common.FirstString = a;
            common.SecondString = b;
            common.Common = ret;
            return common;
        }

        /// <summary>
        /// Return the sentence where words are reversed using C#
        /// </summary>
        /// <param name="sentence"> a sentence consists of words</param>
        /// <returns>Reverse words of the sentence. Return also the input parameter</returns>
        public Reverse ReverseWordsByCSharp(string sentence)
        {
            Reverse reverse = new Reverse();
            string[] words = sentence.Split(' ');
            Array.Reverse(words);

            reverse.Sentence = sentence;
            reverse.ReverseWords = string.Join(" ", words);
            return reverse;
        }

        /// <summary>
        /// Return counts of prime number in the range of [1-N]
        /// </summary>
        /// <param name="range">long number, value of N in [1-N]</param>
        /// <returns>Counts of prime numbers from [1-N]</returns>
        public int CountPrimeByCSharp(long ceilingNumber)
        {
            bool isPrime = true;
            int primeCount = 0;
            for (int i = 2; i <= ceilingNumber; i++)
            {
                for (int j = 2; j <= ceilingNumber; j++)
                {
                    if (i != j && i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    primeCount++;
                }
                isPrime = true;
            }
            return primeCount;
        }
    }
}
