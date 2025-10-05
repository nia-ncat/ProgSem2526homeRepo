using System.ComponentModel.Design;
using System.Net;

namespace sillybilly
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("napiste pocet studentu");
            int pctStudentu = Convert.ToInt32(Console.ReadLine());

            List<int> vek = new List<int>();
            List<string> jmena = new List<string>();
            List<float> prumernaZnamka = new List<float>();

            for (int i = 0; i < pctStudentu; i++)
            {
                Console.WriteLine($"jmeno {i}. studenta?");
                string studentik = Console.ReadLine();
                jmena.Add(studentik);

                Console.WriteLine("vek studenta?");
                int vekk = Convert.ToInt32(Console.ReadLine());
                vek.Add(vekk);

                Console.WriteLine("prumerna znamka studenta?");
                float prmZnamka = float.Parse(Console.ReadLine());
                prumernaZnamka.Add(prmZnamka);
            }

            Console.WriteLine("co chcete delat? (a=vsechny studenty s jejich znamkou, b= pouze studenty s průměrem lepším než 2.0, c= průměr věku všech studentů, d=ukončí program");
            string y = Console.ReadLine();

            while (y != "d")
            {
                if (y == "a")
                {
                    foreach (string l in jmena)
                    {
                        Console.WriteLine($"{l}({vek[jmena.IndexOf(l)]}):{prumernaZnamka[jmena.IndexOf(l)]}");

                    }
                    if (y == "b")
                    {
                        foreach (float m in prumernaZnamka)
                        {
                            if (m < 2.0)
                            {
                                Console.WriteLine($"{jmena[prumernaZnamka.IndexOf(m)]}({vek[prumernaZnamka.IndexOf(m)]}):{m}");
                            }
                        }
                    }
                    if (y == "c")
                    {
                        int prumernyVek = 0;
                        foreach (int n in vek)
                        {
                            prumernyVek += n;
                        }
                        prumernyVek /= pctStudentu;
                        Console.WriteLine($"prumerny vek studentu je {prumernyVek}");

                    }
                }


            }
            Environment.Exit(0);
        }
    }
}
