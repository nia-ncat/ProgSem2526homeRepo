namespace retizek_pratelstvi
{
    internal class Program
    {
        // kod ma casovou i prostorovou slozitost O(n^2) kvuli prohledvani do sirky a matici
        static void Main(string[] args)
        {
            int pocetUzivatelu = Convert.ToInt32(Console.ReadLine());
            string stringPratelstvi = Console.ReadLine();
            bool[,] grafPratelstvi = VytvorMaticiPratelstvi(pocetUzivatelu, stringPratelstvi);
            string potencialniPratele = Console.ReadLine();
            NajdiSpojeniPratel(potencialniPratele, pocetUzivatelu, grafPratelstvi);

        }

        static bool[,] VytvorMaticiPratelstvi(int pocetUzivatelu, string vztahy)
        {
            string[] pratelstvi = vztahy.Split('-', ' ');
            bool[,] maticePratelstvi = new bool[pocetUzivatelu, pocetUzivatelu];
            // menim ty pozice v matici, kde se spojuji pratele na true - jakoze jsou pratele
            for (int pritel = 0; pritel < pratelstvi.Length;)
            {
                maticePratelstvi[Convert.ToInt32(pratelstvi[pritel]) - 1, Convert.ToInt32(pratelstvi[pritel + 1]) - 1] = true;
                maticePratelstvi[Convert.ToInt32(pratelstvi[pritel + 1]) - 1, Convert.ToInt32(pratelstvi[pritel]) - 1] = true;
                pritel += 2;
            }
            return maticePratelstvi;
        }
        static void NajdiSpojeniPratel (string vztah, int pocetOsob, bool[,] maticePratel)
        {
            string[] zkoumaneOsoby = vztah.Split(" ");
            int poziceOsoby1 = Convert.ToInt32(zkoumaneOsoby[0])-1;
            int poziceOsoby2 = Convert.ToInt32(zkoumaneOsoby[1]) - 1;

            bool[] pratelstviSOs1 = new bool[pocetOsob];
            int[] cestaPratelstvi = new int[pocetOsob];
            pratelstviSOs1[poziceOsoby1] = true;
            cestaPratelstvi[poziceOsoby1] = -1;
            bool jsouSpojeni = false;

            Queue<int> fronta = new Queue<int>();
            fronta.Enqueue(poziceOsoby1);

            while (fronta.Count > 0)
            {
                int vrchol = fronta.Dequeue();

                if (vrchol == poziceOsoby2)
                {
                    jsouSpojeni = true;
                    break;
                }

                for (int i = 0; i < pocetOsob; i++)
                {
                    if (maticePratel[vrchol, i] == true && pratelstviSOs1[i] == false)
                    {
                        pratelstviSOs1[i] = true;
                        cestaPratelstvi[i] = vrchol;
                        fronta.Enqueue(i);
                    }
                }
            }

            // zpetna rekonstrukce cesty
            if (!jsouSpojeni)
            {
                Console.WriteLine("neexistuje");
            }
            List<int> cesta = new List<int>();
            int aktualni = poziceOsoby2;

            while (aktualni != -1)
            {
                cesta.Add(aktualni);
                aktualni = cestaPratelstvi[aktualni];
            }

            for (int i = 0; i < cesta.Count; i++)
            {
                cesta[i] = cesta[i] + 1;
            }

            cesta.Reverse();
            Console.WriteLine(string.Join(" ", cesta));
        }

        
    }
}
