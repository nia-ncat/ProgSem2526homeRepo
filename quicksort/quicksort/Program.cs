namespace quicksort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string read = Console.ReadLine();
            string[] list = read.Split(",");
            List<int> ints = new List<int>();
            foreach (string element in list)
            { ints.Add(Convert.ToInt32(element)); }

            Console.WriteLine(string.Join(" ", QuickSort(ints)));
        }

        // BONUS Č 1
        // Rozdelíme celý list na pětice (jestli je konečná pětice neuplná, doplníme nesmírně velkými čísly
        // v každé pětici najdeme medián a pak rekurzivně najdeme medián těch mediánů (? idk bylo to v tom labyrintu)

        // BONUS Č 2 - máme n čísel, vybíráme z něj 3 - pd, že jejich medián je skoro medián?
        // podle toho co jste rikala na hodine:
        // skoromedián = 1/2 na obě strany od mediánu => to tvoří 1/2 n (počítáme i medián do skupiny skoromediánů? odhaduju, že to pořád tvoří 1/2 n)
        //
        // pivot nebude skoromedian za okolnosti, že bu%d všechny tři členi budou mimo skupinu a nebo když dva z nich budou na jednom z těch dvou křídel
        // (> či < než medián)
        // P, že jeden prvek spadne do spatne strany = 1/2
        // P, že všechny prvky nebudou v porstoru skoromedianu = (1/2)^3 = 1/8
        // dodatecny pripad - P, ze 2 prvky budou na jedne strane + jeden bude v prostoru skoromedianu
        // = (1/2 * 1/4 * 1/4) * 2 = 1/16
        // krat dva jelikoz bud muzou byt oba v prave nebo muzou byt oba v leve casti
        // P, ze median tech tri cisel NEbude skoromedian = 1/8 + 1/16 = 3/16
        //
        // P, ze median tech tri cisel BUDE skoromedian = 1 - 3/16 = 13/16
        // minimum nebude v žádném případě (P = 0), protože i kdybychom vybrali jakákoli 2 další čísla tak min nikdy nebude veprostřed

        // BONUS Č 4
        // bude se to výrazně lišit jestli seznam nebude permutace a bude mít VELKÉ mezery mezi sebou
        // jako např. 0,1,2,3,100000000 -> median je 2, ale hodnota nejblizsi k prumeru je 100000000 (coz by mohlo vest k nejhorsi casove slozitosti vyreseni)

        static List<int> QuickSort(List<int> nums)
        {
            if (nums.Count() == 1 || nums.Count() == 0)
            { return nums; }

            int pivot = nums[nums.Count() / 2];

            List<int> left = new List<int>();
            List<int> middle = new List<int>();
            List<int> right = new List<int>();

            for (int i = 0; i < nums.Count(); i++)
            {
                if (nums[i] < pivot)
                { left.Add(nums[i]); }
                else if (nums[i] == pivot)
                { middle.Add(nums[i]); }
                else { right.Add(nums[i]); }
            }

            List<int> result = new List<int>();
            result.AddRange(QuickSort(left));
            result.AddRange(middle);
            result.AddRange(QuickSort(right));

            return result;

        }
    }
}
