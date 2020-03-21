using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace FourthLab
{
    class Program
    {
        class NumberSystems
        {
            public static Lazy<string[]> lazy = new Lazy<string[]>(() => Enumerable.Range('A', 26)
                    .Select(c => ((char)c).ToString())
                    .ToArray());
            public static void SolveWhole(int number, int nbase, out StringBuilder result)
            {
                result = new StringBuilder("");
                bool isPositiveNumber = true;
                if (number < 0)
                {
                    isPositiveNumber = false;
                    number = Math.Abs(number);
                }
                if (nbase > 36)
                {
                    return;
                }
                while (number > 0)
                {

                    int digit = number % nbase;
                    number /= nbase;
                    if (digit >= 10)
                    {
                        result.Append(lazy.Value[digit - 10]);
                    }
                    else
                    {
                        result.Append(Convert.ToString(digit));
                    }

                }
                char[] arr = result.ToString().ToCharArray();
                Array.Reverse(arr);
                string res = new string(arr);
                result.Clear();
                result.Append(res);
                if (!isPositiveNumber)
                {
                    result.Insert(0, '-');
                }
                Console.WriteLine(result);
            }

            public static void Solvefract(double fract, int nbase, out StringBuilder result)
            {
                result = new StringBuilder("");
                Console.WriteLine(fract);
                List<double> fracts = new List<double>();
                fracts.Add(fract);
                for (int i = 0; i <= 100 && fract != 0; i++)
                {
                    double a = Math.Round(fract * nbase, 4);
                    int digit = Convert.ToInt32(Math.Floor(a));
                    fract = a % 1;
                    // Console.WriteLine(a + " " + fract + " " + fracts[0]);
                    int index = fracts.IndexOf(fract);


                    if (digit >= 10)
                    {
                        result = result.Append(lazy.Value[digit - 10]);
                    }
                    else
                    {
                        result = result.Append(Convert.ToString(digit));
                        
                    }
                    if (index != -1)
                        {
                            result.Append(")");
                            result.Insert(index, "(");
                            break;
                        }
                    fracts.Add(fract);
                }
                string res = result.ToString().TrimEnd('0');
                result.Clear();
                result = result.Append(res);
            }
        }
        static void Main(string[] args)
        {
            StringBuilder result = new StringBuilder("");
            string stringNumber, stringBase;
            if (args.Length == 2)
            {
                stringNumber = args[0];
                stringBase = args[1];
            }
            else if (args.Length == 1)
            {
                using (StreamReader sr = new StreamReader(args[0]))
                {
                    string text = sr.ReadToEnd();
                    stringNumber = text.Split(" ")[0];
                    stringBase = text.Split(" ")[1];
                }
            }
            else
            {
                Console.Write("Введите число: ");
                stringNumber = Console.ReadLine();
                Console.Write("Введите систему счисления: ");
                stringBase = Console.ReadLine();
            }
            if (Double.TryParse(stringNumber, out double number) && Int32.TryParse(stringBase, out int nbase) && (nbase >= 2 && nbase <= 36))
            {
                string[] partsOfNumber;
                partsOfNumber = stringNumber.Split(',');

                StringBuilder wholePart = new StringBuilder("");
                NumberSystems.SolveWhole(Convert.ToInt32(partsOfNumber[0]), nbase, out wholePart);
                result.Append(wholePart);
                if (partsOfNumber.Length == 2)
                {
                    partsOfNumber[1] = partsOfNumber[1].TrimEnd('0');
                    partsOfNumber[1] = "0," + partsOfNumber[1];
                    StringBuilder fractional = new StringBuilder("");
                    NumberSystems.Solvefract(Math.Abs(Convert.ToDouble(partsOfNumber[1])), nbase, out fractional);
                    result.Append(",");
                    result.Append(fractional);
                }
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Ошибка ввода");
            }
        }
    }
}
