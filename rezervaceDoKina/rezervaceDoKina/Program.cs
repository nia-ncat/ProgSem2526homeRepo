using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace rezervaceDoKina
{
    internal class Program
    {
        const int pocetRad = 8;
        const int pocetSedadelVRade = 10;
        static int[,] kinoSal = new int[pocetRad, pocetSedadelVRade]; // pocet rad se sedadly, automaticky jsou hodnoty 0
        const int zakladniCenaListku = 180;
        const int vipDodatekKCeneListku = 70;

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                ZobrazHlavniMenu();
                string odpoved = Console.ReadLine();

                if (!new[] { "a", "b", "c" }.Contains(odpoved))
                {
                    Console.WriteLine("neplatny vstup! >:(");
                    continue;
                }
                else if (odpoved == "a")
                { ZobrazKinosal(kinoSal); }
                else if (odpoved == "c")
                { running = false; }
                else  // odpoved je b
                {
                    int rada = 0;
                    int misto = 0;
                    try
                    {
                        Console.WriteLine("do jaké řady si chcete sednout?");
                        rada = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("na jake misto si chcete sednout?");
                        misto = Convert.ToInt32(Console.ReadLine());
                        ZkontrolujRozsah(rada, misto);

                        if (!JeSedadloVolne(kinoSal, rada, misto))
                            throw new InvalidOperationException("Sedadlo je již obsazené.");

                        Console.WriteLine($"cena tohoto listku je {SpocitejCenuListku(rada)} kč  \n chcete si ho koupit?(a/n)");
                        string koupeni = Console.ReadLine();
                        if (koupeni == "a")
                        {
                            ZarezervujSedadlo(kinoSal, rada, misto);
                            Console.WriteLine("super! dekuji moc za nakup :)");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Chyba: {ex.Message}\n");
                    }


                }
            }
        }
        static void ZobrazKinosal(int[,] kinosal)
        {
            Console.WriteLine();
            Console.WriteLine("      PLÁTNO");
            Console.WriteLine(new string('-', 20));
            for (int i = 0; i < kinosal.GetLength(0); i++) //pocet rad 
            { 
                for (int j = 0; j < kinosal.GetLength(1); j++) //pocet sedadel v rade
                {                  
                   Console.Write(kinosal[i, j] + " "); 
                }
                Console.WriteLine();  
            }
            Console.WriteLine("můžete si vybrat ze všech míst označených 0");

        }

        static bool JeSedadloVolne(int[,] kinosal, int rada, int sedadlo)
        {
            if (kinosal[rada-1, sedadlo-1] == 0) 
                return true;
            else return false;
        }

        static int SpocitejCenuListku(int rada)
        {
            if (rada >= 7) // vip řady 7–8 
                return zakladniCenaListku + vipDodatekKCeneListku;

            return zakladniCenaListku;
        }

        static void ZarezervujSedadlo(int[,] kinosal, int rada, int sedadlo)
        {
            kinoSal[rada-1,sedadlo-1] = 1; // zadne kontroly zda obsazene jelikoz to je v jine funkci :)
        }
    
        static void ZobrazHlavniMenu()
        {
            Console.WriteLine("VITEJTE V KINĚ! Co byste si přáli?");
            Console.WriteLine("vidět sál(a) \n zarezervovat místo (b)\n zavrit program(c)");
        }
        static void ZkontrolujRozsah(int rada, int misto)
        {
            if (rada < 0 || rada > pocetRad)
                throw new ArgumentOutOfRangeException("Řada je mimo rozsah.");

            if (misto < 0 || misto > pocetSedadelVRade)
                throw new ArgumentOutOfRangeException("Sedadlo je mimo rozsah.");
        }
    }

    

}
