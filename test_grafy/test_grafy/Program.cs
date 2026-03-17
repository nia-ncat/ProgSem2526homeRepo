namespace test_grafy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("vstupy:");
            int n = Convert.ToInt32(Console.ReadLine());
            List<string> neighbors = new List<string>();
            string vstup = Console.ReadLine();
            while (vstup.Count() == 3)
            { 
                neighbors.Add(vstup);
                vstup = Console.ReadLine();
            }
            int from = Convert.ToInt32(vstup);
            int to = Convert.ToInt32(Console.ReadLine());

            int[,] neighborMatrix = CreateGraphOfRelations(neighbors, n);
        }

        static int[,] CreateGraphOfRelations(List<string> relations, int numOfTowns)
        {
            int[,] result = new int[numOfTowns, numOfTowns];

            foreach(string relation in relations)
            {
                int neighbor1 = Convert.ToInt32(relation[0].ToString());
                int neighbor2 = Convert.ToInt32(relation[2].ToString());
                result[neighbor1 - 1, neighbor2 - 1] = 1;
                result[neighbor2 - 1, neighbor1 - 1] = 1;
            }

            return result;
        }

        
        static List<int> FindAWay(int start, int destination, int[,] neighborhood)
        {
            List<int> weg = new List<int>();
            int numOfNeighbors = neighborhood.GetLength(0);

            int[] openness = new int[numOfNeighbors];
            openness[start - 1] = 0;
            Queue<int> q = new Queue<int>();
            q.Enqueue(start);


            while (q.Count > 0)
            {
                int navstiveneMesto = q.Dequeue();
                openness[navstiveneMesto - 1] = -1;

                for(int i = 0; i < numOfNeighbors; i++)
                {
                    if (neighborhood[navstiveneMesto - 1,i] == 1)
                    {
                        if (openness[i] == 0)
                        {openness[i] = 1;}
                        else if(openness[i] == 1)
                        { }// tady se uz vzdavam
                    }
                    
                }

                for (int nighbor = 0; nighbor < numOfNeighbors; nighbor++)
                {
                    if (openness[nighbor] == 1 && !q.Contains(nighbor + 1))
                    { q.Enqueue(nighbor + 1); }
                }
                
            }


            return weg;
        }
        
        static void PrintResult(int[] vysledek)
        {
            foreach(int v in vysledek) { System.Console.Write($"{v} "); }
        }
    }
}
