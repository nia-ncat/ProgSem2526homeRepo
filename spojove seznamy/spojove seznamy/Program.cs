using System.ComponentModel.Design;

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

            //spojak.Print();
            //Console.WriteLine(spojak.FindMax());
            //spojak.RemoveFromEnd();
            //spojak.Print();
            //spojak.AddToEnd(3);
            //spojak.AddToEnd(7);
            //spojak.AddToEnd(7);
            //spojak.RemoveDuplicates();
            //spojak.Print();
            //spojak.AddToEnd(3);
            //spojak.RemoveSingleVariables();
            //spojak.Print();

            LinkedList listA = new LinkedList();
            listA.AddToEnd(4);
            listA.AddToEnd(1);
            listA.AddToEnd(0);
            listA.AddToEnd(2);
            listA.AddToEnd(3);
            listA.AddToEnd(2);

            LinkedList listB = new LinkedList();
            listB.AddToEnd(0);
            listB.AddToEnd(2);
            listB.AddToEnd(1);
            listB.AddToEnd(8);

            //LinkedList.Union(listA, listB);
            //LinkedList.Intersection(listA, listB);
            LinkedList vysledek = AddingLargeNumbers(listB, listA);
            vysledek.Print();

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

            //fce pro bonus
            public int Length()
            {
                Node currNode = Head;
                int listLength = 0;

                while (currNode != null)
                {
                    currNode = currNode.Next;
                    listLength++;
                }

                return listLength;
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
            //fce 2 pro bonus
            public Node GetToEndOfList()
            {
                Node currNode = Head;

                while (currNode.Next != null) 
                {
                    currNode = currNode.Next;
                }
                return currNode;
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
            public void RemoveAll(int value)
            {
                Node node = Head;


                while (node != null)
                {
                    if (node.Next.Value == value)
                    {
                        node = node.Next.Next;
                    }
                    node = node.Next;
                }

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
                Node prevNode = null;

                while (nodeKPrirovnani != null)
                {
                    // zjistit, jestli má kPri duplikat 
                    Node srovnavaciNode = Head;
                    bool maDuplikat = false;

                    while (srovnavaciNode != null)
                    {
                        // musí být kPri != srov, aby se nezapočítal sám
                        if ( srovnavaciNode != nodeKPrirovnani &&   nodeKPrirovnani.Value == srovnavaciNode.Value)
                        {
                            maDuplikat = true;
                            break;
                        }    
                        srovnavaciNode = srovnavaciNode.Next;
                    }

                    // jestli nema duplikat, musim smazat
                    if (!maDuplikat)
                    {
                        // spec pripad : odstraneni hlavy
                        if (nodeKPrirovnani == Head)
                        {
                            Head = Head.Next;
                            nodeKPrirovnani = Head;

                        }
                        else
                        {
                            prevNode.Next = nodeKPrirovnani.Next;
                            nodeKPrirovnani = nodeKPrirovnani.Next;
                        }

                    }
                    else
                    {
                        // ma duplikat => nic se nedeje, posun dal
                        prevNode = nodeKPrirovnani;
                        nodeKPrirovnani = nodeKPrirovnani.Next;
                    }
                }

            }

            // DESTRUKTIVNI PRUNIK A SJEDNOCENI
            public static void Intersection(LinkedList list1, LinkedList list2)
            {
                // odstranuju duplikaty z jednotlivych seznamu == v pripade, ze by neco bylo v 1 seznamu 2x a tento krok
                // bych neudelala, tak by se to zapocitalo do toho spojeneho 
                list1.RemoveDuplicates();
                list2.RemoveDuplicates();
                // spojuju seznamy
                Node currentNode = list1.Head;
                while (currentNode.Next != null)
                { currentNode = currentNode.Next; }
                currentNode.Next = list2.Head;
                // destruktivne pronikam ? rsv = zustane tam jen ten prunik, rd = bude tam jen jedno z tech cisel pruniku
                list1.RemoveSingleVariables();
                list1.RemoveDuplicates();
            }
            public static void Union(LinkedList list1, LinkedList list2)
            {
                // spojeni seznamu
                Node currentNode = list1.Head;
                while (currentNode.Next != null)
                { currentNode = currentNode.Next; }
                currentNode.Next = list2.Head;
                // odstraneni duplikatu
                list1.RemoveDuplicates();
            }
        }


        static LinkedList AddingLargeNumbers(LinkedList list1, LinkedList list2)
        {
            int zbytek = 0;

            if (list1.Length() < list2.Length())
            {
                LinkedList tmp = list1;
                list1 = list2;
                list2 = tmp;
            }

            int zahloubeniJednotky = list1.Length();

            while (list2.Length() > 0)
            {
                Node konecListu1 = list1.Head;
                for (int j = 0; j < zahloubeniJednotky - 1; j++)
                    konecListu1 = konecListu1.Next;

                Node pridavame = list2.GetToEndOfList();
                konecListu1.Value += pridavame.Value + zbytek;

                if (konecListu1.Value >= 10)
                {
                    konecListu1.Value -= 10;
                    zbytek = 1;
                }
                else
                { zbytek = 0;}

                list2.RemoveFromEnd();
                zahloubeniJednotky--;
            }

            while (zbytek == 1 && zahloubeniJednotky > 0)
            {
                Node curr = list1.Head;
                for (int j = 0; j < zahloubeniJednotky - 1; j++)
                    curr = curr.Next;

                curr.Value += 1;

                if (curr.Value < 10)
                    zbytek = 0;
                else
                {
                    curr.Value = 0;
                    zahloubeniJednotky--;
                }
            }

            if (zbytek == 1)
            {
                Node novy = new Node(1);
                novy.Next = list1.Head;
                list1.Head = novy;
            }

            return list1;
        }



    }
}


