using System.Reflection;

namespace binarni_strom
{
    internal class Program
    {

        static void Main(string[] args)
        {
            BinarySearchTree<Student> tree = new BinarySearchTree<Student>();

            using (StreamReader streamReader = new StreamReader("../../../../studenti_shuffled.csv"))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    string[] studentData = line.Split(',');

                    Student student = new Student(
                        Convert.ToInt32(studentData[0]),    // Id
                        studentData[1],                     // Jméno
                        studentData[2],                     // Příjmení
                        Convert.ToInt16(studentData[3]),    // Věk
                        studentData[4]);                    // Třída

                    // vložíme studenta do stromu, jako klíč slouží jeho Id
                    tree.Insert(student.Id, student);
                    line = streamReader.ReadLine();
                }
            }
            
            // Najděte studenta s ID 20 (David Urban (ID: 20) ze třídy 4.A)
            Student? hledanyStudent = tree.FindValue(20);
            if (hledanyStudent != null)
            { Console.WriteLine(hledanyStudent.ToString()); }
            else 
            { Console.WriteLine("takovy student neexistuje"); }

            // Najděte studenta s nejnižším ID (Kateřina Sedláček (ID: 1) ze třídy 1.B)
            Student? nejnizsiStudent = tree.Min(null).Value;
            if (nejnizsiStudent != null)
            { Console.WriteLine(nejnizsiStudent.ToString()); }
            else
            { Console.WriteLine("strom je prazdny :("); }

            // Vložte vlastního studenta s ID > 100 (je potřeba vytvořit nový objekt typu Student) a zkuste ho pak najít
            Student sasek = new Student(101, "Pasek", "Sasek", 21, "3.C");
            tree.Insert(sasek.Id, sasek);

            // Smažte všechny studenty se sudým ID
            List<int> listOfIDs = tree.CollectAllKeys();
            foreach (int id in listOfIDs)
            {
                if (id % 2 == 0)
                { tree.Pop(id); }
                
            }

            // Vypište strom (měli byste vidět jen ID lichá a seřazená)
            tree.Print();
        }
    }

    class BinarySearchTree<T>
    {
        public Node<T>? Root;

        public void Insert(int newKey, T newValue)
        {

            void _insert(Node<T> node, int newKey, T newValue)
            {
                if (newKey < node.Key) // jdeme doleva
                    if (node.LeftSon == null)
                        node.LeftSon = new Node<T>(newKey, newValue);
                    else
                        _insert(node.LeftSon, newKey, newValue);
                else if (newKey > node.Key) // jdeme doprava
                    if (node.RightSon == null)
                        node.RightSon = new Node<T>(newKey, newValue);
                    else
                        _insert(node.RightSon, newKey, newValue);
                else // našli jsme náš klíč, což bychom neměli, mají být unikátní.... :/
                    throw new Exception(); // vyhodíme chybu
            }

            if (Root == null) // pokud ještě není definován kořen
                Root = new Node<T>(newKey, newValue);
            else
                _insert(Root, newKey, newValue);
        }

        public Node<T>? Min(Node<T>? node)
        {
            if (Root == null)
            {  return null; }
            Node<T> _min(Node<T> node)
            {
                if (node.LeftSon != null)
                { return _min(node.LeftSon); }
                return node;
            }

            if (node == null)
            { return _min(Root); }
            else 
            { return _min(node); }

        }
        public T? FindValue(int key)
        {
            T? _findvalue (Node<T> node, int key)
            {
                if (key > node.Key)
                { return _findvalue(node.RightSon, key); }
                else if (key < node.Key)
                { return _findvalue(node.LeftSon, key); }
                else
                { return node.Value; }
            }
            return _findvalue(Root, key);
            
        }
        public void Pop(int key)
        {
            Node<T> _pop(Node<T> node, int key)
            {
                if (node == null)
                { return null;}
                if (key < node.Key)
                { node.LeftSon = _pop(node.LeftSon, key); }
                else if (key > node.Key)
                { node.RightSon = _pop(node.RightSon, key); }
                else // key == node.key
                {
                    if (node.LeftSon == null && node.RightSon == null) //list
                    { return null; }
                    else if (node.LeftSon == null)
                    { return node.RightSon; }
                    else if (node.RightSon == null)
                    { return node.LeftSon; }
                    else
                    {
                        Node<T> s = Min(node.RightSon);
                        int lostKey = s.Key;
                        T lostValue = s.Value; // nachazime min u praveho odvetvi stromu
                        node.Key = lostKey;
                        node.Value = lostValue;
                        _pop(node.RightSon, s.Key);
                    }
                   

                }
                return node;
            }
            Root = _pop(Root, key);
        }

        public List<int> CollectAllKeys() // potrebne na tu vec se sudymi studenty .. ale pak se i hodilo na print! :)
        {
            List<int> result = new List<int>();
            List<int> _collectAllKeys(Node<T>? node)
            {
                if (node != null)
                {
                    if (node.LeftSon != null)
                    { _collectAllKeys(node.LeftSon); }
                    result.Add(node.Key);
                    if (node.RightSon != null)
                    { _collectAllKeys(node.RightSon); }
                }
                return result;
            }
            return _collectAllKeys(Root);
        }
        public void Print()
        {
            List<int> result = CollectAllKeys();
            if (Root != null)
            { Console.WriteLine(String.Join(" ", result)); }
            else { Console.WriteLine("storm je prazdny"); }
        }
    }

    class Node<T> // T může být libovolný typ
    {
        public Node(int key, T value)
        {
            Key = key;
            Value = value;
        }
        public int Key;
        public T Value;

        public Node<T> LeftSon;
        public Node<T> RightSon;



    }

    class Student
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; }

        public string ClassName { get; }

        public Student(int id, string firstName, string lastName, int age, string className)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            ClassName = className;
        }

        // aby se nám při Console.WriteLine(student) nevypsala jen nějaká adresa v paměti,
        // upravíme výpis objektu typu student na něco čitelného
        public override string ToString()
        {
            return string.Format("{0} {1} (ID: {2}) ze třídy {3}", FirstName, LastName, Id, ClassName);
        }
    }

}
