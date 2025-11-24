namespace spojove_seznamy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LinkedList spojak = new LinkedList();
            spojak.AddToEnd(3);
            spojak.AddToEnd(4);
            spojak.AddToEnd(5);
            spojak.AddToEnd(6);

            spojak.Print();
            Console.WriteLine(spojak.FindMax());
            spojak.RemoveFromEnd();
            spojak.Print();
            spojak.AddToEnd(3);
            spojak.AddToEnd(7);
            spojak.AddToEnd(7);
            spojak.RemoveDuplicates();
            spojak.Print();
            spojak.AddToEnd(3);
            spojak.RemoveSingleVariables();
            spojak.Print();
        }

        class Node
        {
            // konstruktor
            public Node(int value)
            {
                Value = value;
                Next = null;
            }
            public int Value { get; set; }
            public Node? Next { get; set; }
        }

        class LinkedList
        {
            public Node Head { get; set; }
            public void AddToEnd(int value)
            {
                if (Head == null)
                {
                    Head = new Node(value);
                }
                else
                {
                    Node currentNode = Head;
                    while (currentNode.Next != null)
                    {
                        currentNode = currentNode.Next;
                    }
                    currentNode.Next = new Node(value);
                }
            }

            public void Print()
            {
                Node node = Head;
                while (node != null)
                {
                    Console.WriteLine(node.Value);
                    node = node.Next;
                }
                Console.WriteLine(" ");
            }

            // TODO: Najít maximum
            public int? FindMax()
            // int s otazníkem znamená nullovatelný int - může obsahovat číslo i null 
            {
                if (Head == null)
                {
                    Console.WriteLine("Tento seznam je przádný");
                    return null; // nullem naznačíme, že maximum nebylo nalezeno
                }
                else
                {
                    Node node = Head;
                    int x = node.Value;
                    while (node != null)
                    {
                        if (node.Value > x)
                        {
                            x = node.Value;
                        }
                        node = node.Next;
                    }
                    return x;
                }

            }

            // TODO: odebrat prvek z konce
            public void RemoveFromEnd()
            {
                if (Head == null)
                { Console.WriteLine("Tento seznam je prázdný"); }

                if (Head.Next == null)
                { Head = null; }

                else
                {
                    Node secondToLast = Head;

                    while (secondToLast.Next.Next != null)
                    {
                        secondToLast = secondToLast.Next;
                    }
                    secondToLast.Next = null;
                }


            }
            // TODO: najít prvek a vrátit True nebo False, jestli tam je
            public bool FindPrvek(int hledanyPrvek)
            {
                Node currentNode = Head;
                while (currentNode.Next != null)
                {
                    if (currentNode.Value == hledanyPrvek)
                        return true;
                    currentNode = currentNode.Next;
                }
                return false;
            }
            // predtim jeste funkci na odstraneni opakovanych prvku -> intersection
            public void RemoveDuplicates()
            {
                Node nodeKPrirovnani = Head;

                while (nodeKPrirovnani!= null)
                {
                    Node srovnavaciNode = nodeKPrirovnani;
                    while (srovnavaciNode != null && srovnavaciNode.Next != null)
                    {
                        if (nodeKPrirovnani.Value == srovnavaciNode.Next.Value)
                        { srovnavaciNode.Next = srovnavaciNode.Next.Next; }
                        else
                        { srovnavaciNode = srovnavaciNode.Next; }
                    }
                    nodeKPrirovnani = nodeKPrirovnani.Next;
                }

            }
            // + jeste funkci na odstraneni prvku ktere se neopakuji T-T
            public void RemoveSingleVariables()
            {
                Node nodeKPrirovnani = Head;

                while (nodeKPrirovnani != null)
                {
                    Node srovnavaciNode = nodeKPrirovnani;
                    bool maDuplikat = false;
                    while (srovnavaciNode != null)
                    {
                        if (nodeKPrirovnani.Value == srovnavaciNode.Next.Value)
                            maDuplikat = true;
                        else
                            srovnavaciNode = srovnavaciNode.Next;
                    }
                    Node nodeKodstraneni = nodeKPrirovnani;
                    nodeKPrirovnani = nodeKPrirovnani.Next;
                    if (!maDuplikat)
                    {
                        Node currentNode = Head;
                        while (currentNode.Next != nodeKodstraneni)
                        {
                            currentNode = currentNode.Next;
                        }
                        currentNode.Next = currentNode.Next.Next;
                    }
                }
            }

            // DESTRUKTIVNI PRUNIK
            public static void Intersection(LinkedList list1, LinkedList list2)
            {
                list1.RemoveDuplicates();
                list2.RemoveDuplicates();
                Node currentNode = list1.Head;
                while (currentNode.Next != null)
                { currentNode = currentNode.Next; }
                currentNode.Next = list2.Head;
                list1.RemoveSingleVariables();
                list1.RemoveDuplicates();
            }
            public static void Union(LinkedList list1, LinkedList list2)
            {
                Node currentNode = list1.Head;
                while (currentNode.Next != null)
                { currentNode = currentNode.Next; }
                currentNode.Next = list2.Head;
                list1.RemoveDuplicates();
            }
        }


    }
}


