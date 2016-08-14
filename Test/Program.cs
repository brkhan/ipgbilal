using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Program
    {
        public static void Main(string[] argss)
        {
             string[] args =
            {
                "a", "b", "c", "a"
            };
           
            if (args.Length > 0)
            {
                var v = new string[] {"a", "b", "c", "d", "e", "g", "h","d","g","a"};

                var v1 = args[2];

                var v2 = args[3];

                var first = v.Where(w => w == v1).First();

                var last = v.Where(w => w == v2).First();

                var l = Array.IndexOf(v.ToArray(), first);

                var l2 = Array.IndexOf(v.ToArray(), last);

                var l3 = l2 - l;

                var results = v.Skip(l).Take(l3);

                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Arguments invalid");
            }
        }
        }
    }

