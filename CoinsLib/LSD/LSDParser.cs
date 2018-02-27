using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CoinsLib.LSD
{
    public class LSDParser
    {
        public readonly int L;
        public readonly int S;
        public readonly int D;
        public readonly int HD;

        public LSDParser(String s)
        {
            try
            {
                var q = s.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (q.Length == 3)
                {
                    L = ParseLOrS(q[0],'L');
                    S = ParseLOrS(q[1], 'S');
                    if (S >= 20)
                        throw new ArgumentException($"{S} is invalid integer amount for shilling - max is 19");

                    var d = ParseD(q[2]);
                    if (d >= 12)
                        throw new ArgumentException($"{d} is invalid float amount for shilling - max is 11.5");

                    D = (int) d;
                    HD = d > D ? 1 : 0;

                }
                else if (q.Length == 2)
                {
                    L = 0;
                    S = ParseLOrS(q[0], 'S');
                    if (S >= 20)
                        throw new ArgumentException($"{S} is invalid integer amount for shilling - max is 19");

                    var d = ParseD(q[1]);
                    if (d >= 12)
                        throw new ArgumentException($"{d} is invalid float amount for shilling - max is 11.5");

                    D = (int)d;
                    HD = d > D ? 1 : 0;
                }
                else if (q.Length == 1)
                {
                    L = 0;
                    S = 0;
                    var d = ParseD(q[0]);
                    if (d >= 12)
                        throw new ArgumentException($"{d} is invalid float amount for shilling - max is 11.5");

                    D = (int)d;
                    HD = d > D ? 1 : 0;
                }
                else
                {
                    throw new ArgumentException($"{s} is invalid input to LSDParser - expected, e.g., '10/3' or '3.5d'");
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException($"Error parsing - '{s}'", e);
            }
        }

        int ParseLOrS(string s,char LorS)
        {
            if (s == "-")
                return 0;

            foreach (char c in s)
                if (Char.IsNumber(c) == false)
                    throw new ArgumentException($"Invalid character found in '{LorS}' position of string - '{s}'");

            return Int32.Parse(s);
        }

        float ParseD(string s)
        {
            if (s == "-")
                return 0;

            foreach (char c in s)
                if (Char.IsNumber(c) == false && c != '.' && c != 'd')
                    throw new ArgumentException($"Invalid character found in 'D' position of string - {s}");

            if (s.Contains(".") && (char.IsNumber(s.Last()) && s.Last() != '5'))
                throw new ArgumentException($"Invalid character found in '.5' position of string - {s} - expected .5");

            if (s.Contains(".") && (!char.IsNumber(s.Last()) && s.Last() != 'd'))
                throw new ArgumentException($"Invalid character found in 'd' position of string - {s} - expected only 'd' or '.5' at end of string");


            if (s.Contains("d") && s.Last() != 'd')
                throw new ArgumentException($"Invalid character found in 'd' position of string - {s} - expected 'd' at end of string ");

            if (s.Contains("d"))
                s = s.Substring(0, s.Length - 1);

            return float.Parse(s);
        }

        public int ValueInHalfD()
        {
            return HD*1 + D*2 + S*2*12 + L*2*12*20;

        }
    }
}
