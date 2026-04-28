namespace notace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        static double PostfixCount(string postfix)
        {
            string[] opers = postfix.Split(' ');
            Stack<double> stack = new Stack<double>();
            foreach (string op in opers)
            {
                if (stack.Count >= 2)
                {
                    if (op == "+")
                    {
                        double cislo1 = stack.Pop();
                        double cislo2 = stack.Pop();
                        stack.Push(cislo1 + cislo2);
                    }
                    else if (op == "-")
                    {
                        double cislo1 = stack.Pop();
                        double cislo2 = stack.Pop();
                        stack.Push(cislo2 - cislo1);
                    }
                    else if (op == "*")
                    {
                        double cislo1 = stack.Pop();
                        double cislo2 = stack.Pop();
                        stack.Push(cislo2 * cislo1);
                    }
                    else if (op == "/")
                    {
                        double cislo1 = stack.Pop();
                        double cislo2 = stack.Pop();
                        if(cislo1 == 0)
                            throw new Exception("deleni nulou neni definovano!"); 
                        else
                            stack.Push(cislo2 / cislo1);
                    }
                    else
                    {
                        if (double.TryParse(op, out double cislo))
                            stack.Push(cislo);
                        else
                            throw new Exception("hej! to nepatri v normalnim prikladu >:(");
                    }
                }
                else
                {
                    if (op == "*" || op == "/" || op == "+" || op == "-")
                         throw new Exception("špatný vstup: máte příliš hodně operátorů/je to spatne poskladane"); 
                    else
                    {
                        if (double.TryParse(op, out double cislo))
                            stack.Push(cislo);
                        else
                            throw new Exception("hej! to nepatri v normalnim prikladu >:(");
                    }
                }
            }

            if (stack.Count == 1)
            { return stack.Pop(); }
            else { throw new Exception("neco tady nevychazi.."); }
        }

        // UDELATTTTTT PREFIX !!!! DOCELA JINA LOGIKA
        static double PrefixCount(string prefix)
        {
            string[] opers = prefix.Split(' ');
            Stack<string> stack = new Stack<string>();
            string[] ooperators = [ "+", "-", "*", "/" ];

            foreach (string op in opers)
            {
                if (ooperators.Contains(op)) // ?? zeptat se mazne
                { stack.Push(op); }
                else
                {
                    if (!double.TryParse(op, out double cislo2))
                        { throw new Exception("hej! to nepatri v normalnim prikladu >:("); }
                    else if (!double.TryParse(stack.Peek(), out double cislo1))
                    { stack.push(op); }
                    else
                    {
                        stack.Pop();
                        if (stack)

                    }
                }
            }

            if (stack.Count == 1)
            { return stack.Pop(); }
            else { throw new Exception("neco tady nevychazi.."); }
        }
    }
}
