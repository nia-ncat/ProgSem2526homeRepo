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
            int max = ciselnePole[0];
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
            int indexNejvicNaLevo = 0;
            int indexNejvicNaPravo = serazenePole.Length - 1;
            int stred;

            if  ((serazenePole[0] > hledaneCislo)||(serazenePole[indexNejvicNaPravo] < hledaneCislo))
                return -1;

            while (indexNejvicNaPravo >= indexNejvicNaLevo)
            {
                stred = (indexNejvicNaLevo + indexNejvicNaPravo) / 2;
                if (hledaneCislo == serazenePole[stred])
                { return stred; }
                if (hledaneCislo > serazenePole[stred])
                { indexNejvicNaPravo = stred - 1; }
                else
                { indexNejvicNaLevo = stred + 1; }
            }
            return -1;

        }
    }
}
