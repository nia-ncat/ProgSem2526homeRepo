using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace topological_sort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // definice pomocnych promen
            Console.WriteLine("vztahy:");
            string[] vztahy = Console.ReadLine().Split();
            int[,] maticeVztahu = VytvorMaticiVztahu(vztahy);
            for(int i = 0; i<maticeVztahu.Length; i++)
            {
                for (int j = 0; j<maticeVztahu.Length; j++)
                { Console.Write(maticeVztahu[i,j]); }
                Console.WriteLine();
            }
        }

        static List<char> NajdiVsechnyZnakyAbecedy(string[] precteneVztahy)
        {
            List<char> znakyAbecedy = new List<char>();
            for (int i = 0; i < precteneVztahy.Length; i++)
            {
                string vztah = precteneVztahy[i];
                foreach(char znak in vztah)
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
                {delka = l;}
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
            int cisloPozice = 0;
            while (cisloPozice<maxLen)
            {
                
                for (int slovo = 0; slovo < precteneVztahy.Length; slovo++)
                {
                    for (int druheSlovo = 1; druheSlovo < precteneVztahy.Length;druheSlovo++)
                    {
                        if (precteneVztahy[slovo].Length >= cisloPozice + 1 && precteneVztahy[druheSlovo].Length >= cisloPozice + 1) // zda maji slova vubec nejake pismenko ke srovnani
                        {
                            bool stejne = true;
                            for (int o = 0; o < cisloPozice; o++)
                            {   // zda ta pismena pred pismenem na pozici jsou stejna -> pak muzu srovnavat 
                                // jako v slovniku :)
                                if (precteneVztahy[slovo][o] != precteneVztahy[druheSlovo][o])
                                { stejne = false; }
                            }
                            // mozna by se ty podminky mohly zapsat lip ale toto je zp ktery chapu :')
                            if (stejne && precteneVztahy[slovo][cisloPozice] != precteneVztahy[druheSlovo][cisloPozice])
                            {
                                int indexZ = znakyAbecedy.IndexOf(precteneVztahy[slovo][cisloPozice]);
                                int indexDo = znakyAbecedy.IndexOf(precteneVztahy[druheSlovo][cisloPozice]);
                                graf[indexZ, indexDo] = 1;
                            }
                        }
                    }
                }
                cisloPozice++;

            }

            return graf;

        }
    }
}
