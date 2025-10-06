namespace funkce_pole_cisel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] pole1 = [5, 6, 2, 80, 4];

            // testovani ulohy findmax
            int nejvetsiCislo=FindMax(pole1);
            Console.WriteLine(nejvetsiCislo);

            //testovani ulohy sortarray
            int[] sortedPole = SortArray(pole1);
            Console.WriteLine(string.Join(", ", sortedPole)); // vypsani seznamu pole (ugh proc se to nemuze vypsat normalne, jako tohle dava smysl ale O_O)

            // testovani binary search
            int hledamSestku = BinarySearch(pole1, 6);
            int hledamPetku = BinarySearch(pole1, 5);
            Console.WriteLine(hledamSestku);
            Console.WriteLine(hledamPetku);
        }
        static int FindMax(int[] ciselnePole)
        {
            int max = 0;
            foreach (int i in ciselnePole)
                if (i > max)
                    max = i;
            return max;
        }

        static int[] SortArray(int[] fieldOfNumbers)
        {
            // pokus o insertion sort  - porad n na druhou jako bubble sort ale proc ne
            int[] usporadanwPole = fieldOfNumbers.ToArray(); // vytvoreni noveho pole bez zmeny stareho

            int i = 1; 
            while (i < usporadanwPole.Length) 
            {
                int hodnotaI = usporadanwPole[i]; 
                int j = i; 
                while (j > 0 && usporadanwPole[j - 1] > hodnotaI) // merime zda jsou cisla pred hodnotou vetsi ci ne
                {
                    usporadanwPole[j] = usporadanwPole[j - 1];
                    j--;
                }
                usporadanwPole[j] = hodnotaI;
                i++;
            }
            return usporadanwPole;
        }

        static int BinarySearch(int[] serazenePole, int hledaneCislo)
        {
            int i = 0;
            int indexHledanehoCisla = 0;
            List<int> pomocnePoleAsi = new List<int>(serazenePole); // list protoze jsem potrebovala funkci, ktera funguje v typu list (radek 69 heh)
           
            while (i != hledaneCislo && pomocnePoleAsi.Count != 1)
            {
                i = pomocnePoleAsi[pomocnePoleAsi.Count / 2];
                
                if (i > hledaneCislo)
                {
                    pomocnePoleAsi.RemoveRange(0, pomocnePoleAsi.IndexOf(i) + 1);
                }
                else if (i < hledaneCislo)
                {
                    pomocnePoleAsi.RemoveRange(pomocnePoleAsi.IndexOf(i), pomocnePoleAsi.Count - (pomocnePoleAsi.IndexOf(i)+1)); //  odstranime tu cast seznamu kde urcite vime, ze neni hledane cislo
                    
                }

            }

            if (i == hledaneCislo)
            {
                indexHledanehoCisla = Array.FindIndex(serazenePole, x => x == hledaneCislo);
            }
            else if (pomocnePoleAsi.Count == 1)
            {
                indexHledanehoCisla = -1;
            }

                return indexHledanehoCisla;
        }
    }
}
