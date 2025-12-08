using System.Security.Cryptography;

namespace trojuhelnik_test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string vstup = Console.ReadLine();
            bool pokracovat = true;
            while (pokracovat)
            {
                if (vstup == "q")
                {
                    pokracovat = false;
                }
                else
                {
                    List<int> vrchol1 = new List<int>();
                    vrchol1.Add(Convert.ToInt32(vstup));
                    vrchol1.Add(Convert.ToInt32(Console.ReadLine()));
                    List<int> vrchol2 = new List<int> ();
                    vrchol2.Add(Convert.ToInt32(Console.ReadLine()));
                    vrchol2.Add(Convert.ToInt32(Console.ReadLine()));
                    List<int> vrchol3 = new List<int> ();
                    vrchol3.Add(Convert.ToInt32(Console.ReadLine()));
                    vrchol3.Add(Convert.ToInt32(Console.ReadLine()));

                    double strana1a2 = VypoctiDelku(vrchol1, vrchol2);
                    double strana1a3 = VypoctiDelku(vrchol1, vrchol3);
                    double strana3a2 = VypoctiDelku(vrchol3, vrchol2);

                    if (strana1a2 + strana1a3 > strana3a2 && strana3a2 + strana1a3 > strana1a2 && strana3a2 + strana1a2 > strana1a3)
                    {
                        Console.WriteLine(strana1a2);
                        Console.WriteLine(strana3a2);
                        Console.WriteLine(strana1a3);
                    }
                    else
                    {
                        Console.WriteLine("tyto tri body nevytvori trojuhelnik");
                    }

                }
            }

        }
        static double VypoctiDelku(List<int> bod1, List<int> bod2)
        {
            double delka = 0;
            int[] indexy = [0, 1];
            foreach (int index in indexy)
            {
                float vektorovaSlozka = bod1.ElementAt(index) - bod2.ElementAt(index);
                delka =+ Math.Pow(vektorovaSlozka,2);
            }
            double vzdalenost = (int)Math.Sqrt(delka);
            return vzdalenost;
        }
    }
}
