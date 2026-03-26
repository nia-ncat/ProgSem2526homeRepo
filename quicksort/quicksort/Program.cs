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

            Console.WriteLine(string.Join(", ", QuickSort(ints)));
        }

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
