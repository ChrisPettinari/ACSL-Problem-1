using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ACSL_Problem_1
{
    class Program
    {
        public static int cardValue (String card)
        {
            return "0123456789TJQKA".IndexOf(card.Trim());
        }

        static void Main(string[] args)
        {
            //string[] input = File.OpenRead(@"C:\Users\eahscs\Desktop\sampleinput.txt");
            List<String> lines = File.ReadAllLines("data.txt").Select(n => n.Trim().Replace(" ", "")).ToList();

            List<int> hands = lines[0].Split(new char[] { ',' }).Select(n => cardValue(n)).ToList();
            lines.RemoveAt(0);

            foreach (string line in lines)
            {
                List<int> p1 = hands.GetRange(0, 5);
                List<int> p2 = hands.GetRange(5, 5);

                p1.Sort();
                p2.Sort();

                int total = Convert.ToInt32(line.Substring(0, line.IndexOf(',')));
                int[] values = line.Substring(line.IndexOf(',')+1).Split(new char[] { ',' }).Select(n => cardValue(n)).ToArray();

                for (int i = 0; i < values.Length; i++)
                {
                    // Do player 1 stuff
                    total += p1[2];

                    p1[2] = values[i];
                    p1.Sort();

                    if (total > 99)
                    {
                        // who wins?
                        break;
                    }

                    // Do player 2 stuff
                    i++;

                }
            }


            //string[] line1 = input.Replace(", ", ",").Split(",");


        }
    }
}