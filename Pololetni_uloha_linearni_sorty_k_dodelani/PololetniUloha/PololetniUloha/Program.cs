using PololetniUloha;
using System.Globalization;
using System.Text;

namespace PololetniUloha
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // (20b) 1. Seřaďte známky ze souboru znamky.txt od 1 do 5 algoritmem s lineární časovou složitostí vzhledem k počtu známek. 
            // Vypište je na řádek a pak vypište i četnosti jednotlivých známek.


            int[] pocetZnamek = { 0, 0, 0, 0, 0 };

            using (StreamReader sr = new StreamReader(@"..\..\..\..\..\znamky.txt")) // otevření souboru pro čtení
            {
                while (!sr.EndOfStream) // dokud jsme nedošli na konec souboru
                {
                    int znamka = Convert.ToInt16(sr.ReadLine()); // čteme známky po řádcích a převádíme je na číslo
                    pocetZnamek[znamka - 1] = +1;
                }
            }
            // => to, co jste pravděpodobně stvořili se nazývá Counting Sort

            //vypsani studentu
            for (int znamak = 0; znamak < pocetZnamek.Length; znamak++)
            {
                for (int i = 0; i < pocetZnamek[znamak]; i++)
                { Console.Write(znamak + 1); }
            }
            Console.WriteLine();
            for (int znamecka = 0; znamecka < pocetZnamek.Length; znamecka++)
            {
                Console.WriteLine($"{pocetZnamek[znamecka]} studentu dostalo {znamecka + 1}");
            }


            // (40b) 2. Ze souboru znamky_prezdivky.csv vytvořte objekty typu Student se správně přiřazenou známkou a přezdívkou.
            // Seřaďte je podle známek (stabilně = dodržte pořadí v souboru) a vypište seřazené dvojice (znamka: přezdívka) - na každý řádek jednu.
            using (StreamReader sr = new StreamReader(@"..\..\..\..\..\znamky_prezdivky.csv"))
            {
                    List<Student>[] znamkyStudentu =
                    {
                        new List<Student>(), new List<Student>(), new List<Student>(), new List<Student>(), new List<Student>()
                    };
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(";");
                    // prevadime to, co jsme precetli na nami urceny class studeet
                    Student studentik = new Student(line[0], Convert.ToInt32(line[1]));
                    // pridavame studenta do databaze znamkyStudentu podle jeho znamky
                    znamkyStudentu[studentik.Znamka - 1].Add(studentik);
                }
                for (int i = 0; i<znamkyStudentu.Length; i++)
                {
                    for (int j = 0; j < znamkyStudentu[i].Count(); j++)
                    {
                        Student danyStudent = znamkyStudentu[i][j];
                        Console.WriteLine($"{danyStudent.Znamka}: {danyStudent.Prezdivka}");
                    }
                }
            }
            // => to, co jste pravděpodobně stvořili se nazývá Bucket Sort (přihrádkové řazení)

            // (10b) 3. Určete časovou a prostorovou složitost algoritmu z 2. úkolu
            // ☆ muj odhad je casova slozitost linearni a prostorova taky?



            // (+60b) 4. BONUS: Napište kód, který bude řadit lexikograficky velká čísla v lineárním čase. Využijte dat ze souboru velka_cisla.txt

           // DODELAM NEKDY JINDY
        }
    }

    class Student
    {
        public string Prezdivka { get; } // tím, že je zde pouze get říkáme, že tato vlastnost třídy Student jde mimo třídu pouze číst, nikoli upravovat
        public int Znamka { get; }
        public Student(string prezdivka, int znamka) // konstruktor třídy
        {
            // použitím samotného { get; } také říkáme, že tyto vlastnosti jdou nastavit nejpozději v konstruktoru - tedy v této metodě
            Prezdivka = prezdivka;
            Znamka = znamka;
        }
    }
}
