
using System.Data;
using System.Linq.Expressions;

namespace topological_sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // definice pomocnych promen
            Console.WriteLine("vztahy:");
            string[] vztahy = Console.ReadLine().Split(" ");
            int[,] maticetvl = VytvorMaticiVztahu(vztahy);
            for (int i = 0; i < maticetvl.GetLength(0); i++)
            {
                for (int j = 0; j < maticetvl.GetLength(0); j++)
                { Console.Write(maticetvl[i, j]); }
                Console.WriteLine();
            }
            char[]? vysledek = UrciPoradi(vztahy);
            if (vysledek == null)
            {
                Console.WriteLine("Graf obsahuje cyklus, nelze topologicky urcit poradi");
            }
            else
            {
                Console.WriteLine(string.Join(" -> ", vysledek));
            }

            // kontrola se vstupem "acb abc cab bac"

            // BONUS: obecne u typologickych usporadani to neni jednoznacne, jelikoz tam muzou byt vrcholy se stejnym in-degree,  
            // ALE jelikoz tento program to proste bere po poradi, tak to podle me je jednoznacne
            // BONUS 2: priklad neurcitelneho vstupu: abc abb cb ba

        }

        // PREVEDENI INPUTU NA SROZUMITELNY VSTUP - AKA MATICI VZTAHU
        static List<char> NajdiVsechnyZnakyAbecedy(string[] precteneVztahy)
        {
            List<char> znakyAbecedy = new List<char>();
            for (int i = 0; i < precteneVztahy.Length; i++)
            {
                string vztah = precteneVztahy[i];
                foreach (char znak in vztah)
                {
                    if (!znakyAbecedy.Contains(znak))
                        znakyAbecedy.Add(znak);
                }
            }
            return znakyAbecedy;
        }
        static int ZjistiDelkuNejdelsihoSlova(string[] pole) // potrebuju na vytvoreni matice .. 
        {
            int delka = 0;
            for (int i = 0; i < pole.Length; i++)
            {
                int l = pole[i].Length;
                if (delka < l)
                { delka = l; }
            }
            return delka;
        }
        static int[,] VytvorMaticiVztahu(string[] precteneVztahy)
        {
            List<char> znakyAbecedy = NajdiVsechnyZnakyAbecedy(precteneVztahy);
            int pocetVrcholu = znakyAbecedy.Count();
            int[,] graf = new int[pocetVrcholu, pocetVrcholu];

            int maxLen = ZjistiDelkuNejdelsihoSlova(precteneVztahy);
            // potrebuju vlastne srovnavat znaky, ktere jsou na stejne pozici v jinych slovech 
            int indexZkoumanePozice = 0;
            while (indexZkoumanePozice < maxLen)
            {

                for (int indexSlova = 0; indexSlova < precteneVztahy.Length; indexSlova++)
                {
                    for (int indexDalsihoSlova = indexSlova + 1; indexDalsihoSlova < precteneVztahy.Length; indexDalsihoSlova++)
                    {
                        if (precteneVztahy[indexSlova].Length >= indexZkoumanePozice + 1
                            && precteneVztahy[indexDalsihoSlova].Length >= indexZkoumanePozice + 1) // zda maji slova vubec nejake pismenko ke srovnani
                        {
                            bool pismenaSeShoduji = true;
                            for (int indexPismene = 0; indexPismene < indexZkoumanePozice; indexPismene++)
                            {   // zda ta pismena pred pismenem na pozici jsou stejna -> pak muzu srovnavat 
                                // jako v slovniku :)
                                if (precteneVztahy[indexSlova][indexPismene] != precteneVztahy[indexDalsihoSlova][indexPismene])
                                { pismenaSeShoduji = false; }
                            }
                            // mozna by se ty podminky mohly zapsat lip ale toto je zp ktery chapu :')
                            if (pismenaSeShoduji && precteneVztahy[indexSlova][indexZkoumanePozice] != precteneVztahy[indexDalsihoSlova][indexZkoumanePozice])
                            {
                                int indexZ = znakyAbecedy.IndexOf(precteneVztahy[indexSlova][indexZkoumanePozice]);
                                int indexDo = znakyAbecedy.IndexOf(precteneVztahy[indexDalsihoSlova][indexZkoumanePozice]);
                                graf[indexZ, indexDo] = 1;
                            }
                        }
                    }
                }
                indexZkoumanePozice++;

            }

            return graf;

        }

        // POKUS O VYUZITI KAHNOVA ALGORITM
        static int[] CountInDegrees(int[,] maticeVztahu)
        {
            int[] inDegrees = new int[maticeVztahu.GetLength(0)];
            for (int i = 0; i < maticeVztahu.GetLength(0); i++)
            {
                for (int j = 0; j < maticeVztahu.GetLength(0); j++)
                {
                    if (maticeVztahu[i, j] == 1)
                    { inDegrees[j]++; }
                }
            }
            return inDegrees;
        }
        static char[]? UrciPoradi(string[] slovnikovyInput)
        {
            int[,] maticeVztahu = VytvorMaticiVztahu(slovnikovyInput);
            List<char> znaky = NajdiVsechnyZnakyAbecedy(slovnikovyInput);
            int[] inDegrees = CountInDegrees(maticeVztahu);

            Queue<char> znakyBezInDegrees = new Queue<char>();
            for (int i = 0; i < inDegrees.Length; i++)
            {
                if (inDegrees[i] == 0)
                { znakyBezInDegrees.Enqueue(znaky[i]); }
            }

            int index = 0; // taky i pocet vyuzitych promen
            char[] vyslednePoradi = new char[znaky.Count()]; 

            while(znakyBezInDegrees.Count > 0)
            {
                char odstranenyZnak = znakyBezInDegrees.Dequeue();
                vyslednePoradi[index] = odstranenyZnak;
                // upraveni vztahu po tom, co jsme odstranili znak
                int indexOdstranenehoZnaku = znaky.IndexOf(odstranenyZnak);
                for (int i = 0; i < znaky.Count ; i++)
                {
                    if (maticeVztahu[indexOdstranenehoZnaku, i] == 1)
                    { 
                        inDegrees[i]-- ; 
                        if (inDegrees[i] == 0)
                        { znakyBezInDegrees.Enqueue(znaky[i]); }
                    }
                }
                index++;
            }
            if (index != znaky.Count)
            { return null; } // graf obsahuje cyklus!
            return vyslednePoradi;

        }
    }
}