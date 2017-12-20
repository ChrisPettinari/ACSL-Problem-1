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

        public static bool crossesBorder (int start, int adding)
        {
            if ((adding == 10) && (start > 34 && start - adding < 33) || (start > 56 && start - adding < 55) || (start > 78 && start + adding < 77))
            {
                return true;
            }

            if ((start < 33 && start + adding > 34) || (start < 55 && start + adding > 56) || (start < 77 && start + adding > 78))
            {
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
           
            List<String> lines = File.ReadAllLines("data.txt").Select(n => n.Trim().Replace(" ", "")).ToList();
            // gets the input into a list of strings

            List<int> hands = lines[0].Split(new char[] { ',' }).Select(n => cardValue(n)).ToList();
            //gets the list of strings and puts it into a list of ints

            lines.RemoveAt(0);
            //removes the first line in the string list

            foreach (string line in lines)
            {
                //runs through every line in the string arry

                List<int> p1 = hands.GetRange(0, 5);
                //gets the hand for player 1
                List<int> p2 = hands.GetRange(5, 5);
                //gets the hand for player 2

                p1.Sort();
                //sorts player 1's from least to greatest
                p2.Sort();
                //sorts player 2's hand from least to greatest

                int total = Convert.ToInt32(line.Substring(0, line.IndexOf(',')));
                //gets intital point total
                int[] values = line.Substring(line.IndexOf(',')+1).Split(new char[] { ',' }).Select(n => cardValue(n)).ToArray();
                //gets the values of the cards in each hand

                for (int i = 0; i < values.Length + 1; i++)
                {
                    // Do player 1 stuff
                    /*total += p1[2];

                    p1[2] = values[i];
                    p1.Sort();*/
                    List<int> player = p1;

                    if (i % 2 == 1)
                        player = p2;
                    else
                        player = p1;

                    player.Sort();

                    if (player[2] == 9)
                        total = total;
                    else if (player[2] == 10)
                    {
                        if (crossesBorder(total, 10))
                        {
                            total += 5;
                        }

                        total -= 10;
                    }
                    else if (player[2] == 7)
                    {
                        if (total + 7 >= 99)
                            total += 1;
                        else if (player[2] == 7 && total + 7 < 99)
                        {
                            if (crossesBorder(total, 7))
                                total += 5;

                                total += 7;
                        }
                    }
                    else
                    {
                        total += player[2];
                    }

                    if (crossesBorder(total, player[2]))
                    {
                        total += 5;
                    }

                    player.Sort();

                    if (total > 99)
                    {
                        if (player == p1)
                        {
                            Console.WriteLine( total + ", Player #1");
                        }
                        else if (player == p2)
                        {
                            Console.WriteLine(total + ", Player #2");
                        }
                        break;
                    }

                    player[2] = values[i];
                }
            }


            //string[] line1 = input.Replace(", ", ",").Split(",");


        }
    }
}