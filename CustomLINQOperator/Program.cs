namespace CustomLINQOperator {
    static class CustomEnumerableExtensions
    {
        public static IEnumerable<T> LastN<T>(this IEnumerable<T> source, int n)
        {
            Queue<T> last = new();
            foreach (T item in source)
            {
                if (last.Count >= n)
                {
                    last.Dequeue();
                }
                last.Enqueue(item);
            }
            foreach (T item in last)
            {
                yield return item;
            }
        }
        public static string ToCSV(this IEnumerable<int> source)
        {
            return string.Join(", ", source.Select(i => i.ToString() ?? string.Empty));
        }
        public static IEnumerable<T> IntersectSorted<T>(this IEnumerable<T> a, IEnumerable<T> b)
            where T : IComparable<T>
        {
            IEnumerator<T> aIterator = a.GetEnumerator();
            IEnumerator<T> bIterator = b.GetEnumerator();
            if(!aIterator.MoveNext() || !bIterator.MoveNext())
            {
                yield break;
            }
            while(true)
            {
                int comparison = aIterator.Current.CompareTo(bIterator.Current);
                if(comparison == 0)
                {
                    yield return aIterator.Current;
                    if(!aIterator.MoveNext() || !bIterator.MoveNext())
                    {
                        yield break;
                    }
                }
                else if(comparison < 0)
                {
                    if(!aIterator.MoveNext())
                    {
                        yield break;
                    }
                }
                else
                {
                    if(!bIterator.MoveNext())
                    {
                        yield break;
                    }
                }
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Approach1();
            Approach2();
            Approach3();
        }

        public static void Approach1()
        {
            IEnumerable<int> numbers = Enumerable.Range(1, 100);
            Queue<int> last = new();
            foreach (int number in numbers)
            {
                if (last.Count >= 12)
                {
                    last.Dequeue();
                }
                last.Enqueue(number);
            }
            System.Console.WriteLine(string.Join(", ", last.Select(i => i.ToString() ?? string.Empty)));
        }

        public static void Approach2()
        {
            System.Console.WriteLine(Enumerable.Range(1, 100).LastN(12).ToCSV());
        }

        public static void Approach3() 
        {
            IEnumerable<int> a = Enumerable.Range(1, 100);
            IEnumerable<int> fibonacci = new [] { 0, 1 , 2, 3, 5 , 8, 13, 21, 34, 55, 89, 144, 233, 377 };
            System.Console.WriteLine(fibonacci.IntersectSorted(a).ToCSV());
        }
    }
}