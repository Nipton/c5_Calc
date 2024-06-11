namespace c5_Calc
{
    interface ICalc
    {
        event EventHandler<EventArgs>? GotResult;
        void Add(int i);
        void Sub(int i);
        void Div(int i);
        void Mul(int i);
        bool CancelLast();
    }
    public class Calculator : ICalc
    {
        public event EventHandler<EventArgs>? GotResult;
        private int result = 0;
        public int Result { get => result; }
        Stack<int> stackResults = new Stack<int>();
        public void Add(int i)
        {
            result += i;
            stackResults.Push(result);
            GotResult?.Invoke(this, EventArgs.Empty);
        }
        public void Sub(int i)
        {
            result -= i;
            stackResults.Push(result);
            GotResult?.Invoke(this, EventArgs.Empty);
        }
        public void Div(int i)
        {
            result /= i;
            stackResults.Push(result);
            GotResult?.Invoke(this, EventArgs.Empty);
        }

        public void Mul(int i)
        {
            result *= i;
            stackResults.Push(result);
            GotResult?.Invoke(this, EventArgs.Empty);
        }
        public bool CancelLast()
        {
            if(stackResults.Count > 0)
            {
                stackResults.Pop();  
                result = stackResults.TryPeek(out int a) ? a : 0;
                GotResult?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }
    }


    internal class Program
    {
        static void PrintResult(object sender, EventArgs e)
        {
            Console.WriteLine("Результат: " + (sender as Calculator)?.Result);
        }
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            calc.GotResult += PrintResult;

            while (true) 
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1) Сложение.");
                Console.WriteLine("2) Вычитание.");
                Console.WriteLine("3) Умножение.");
                Console.WriteLine("4) Деление.");
                Console.WriteLine("5) Отменить последнюю операцию.");
                Console.WriteLine("0) Выход.");
                string input = Console.ReadLine()!; 
                switch (input)
                {
                    case "1":
                        Console.Write("Введите число: ");
                        if(int.TryParse(Console.ReadLine(), out int result))
                        {
                            Console.Clear();                           
                            calc.Add(result);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Неверный ввод.");
                            Console.WriteLine("\nРезультат: " + calc.Result);
                        }
                        break;
                    case "2":
                        Console.Write("Введите число: ");
                        if (int.TryParse(Console.ReadLine(), out int result2))
                        {
                            Console.Clear();
                            calc.Sub(result2);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Неверный ввод.");
                            Console.WriteLine("\nРезультат: " + calc.Result);
                        }
                        break;
                    case "3":
                        Console.Write("Введите число: ");
                        if (int.TryParse(Console.ReadLine(), out int result3))
                        {
                            Console.Clear();
                            calc.Mul(result3);
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Неверный ввод.");
                            Console.WriteLine("\nРезультат: " + calc.Result);
                        }
                        break;
                    case "4":
                        Console.Write("Введите число: ");
                        if (int.TryParse(Console.ReadLine(), out int result4))
                        {
                            if (result4 == 0)
                            {
                                Console.Clear();
                                Console.WriteLine("Невозможно деление на ноль.");
                                Console.WriteLine("\nРезультат: " + calc.Result);
                            }
                            else
                            {
                                Console.Clear();
                                calc.Div(result4);
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Неверный ввод.");
                            Console.WriteLine("\nРезультат: " + calc.Result);
                        }
                        break;
                    case "5":
                        Console.Clear();
                        if (calc.CancelLast())
                        {                           
                            Console.WriteLine("Последняя операция отменена.\n");
                        }
                        else
                        {
                            Console.WriteLine("Нет операции, которую можно отменить.\n");
                        }
                        break;
                    case "0":
                        return;
                    case "":
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверный ввод.\n");
                        break;
                }
            }
        }
    }
}