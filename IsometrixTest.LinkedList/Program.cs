using System;

namespace IsometrixTest.LinkedList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomLinkedList<int> list = new CustomLinkedList<int>();

            // inserting - valid positions
            list.Insert(0, 0);
            list.Insert(2, 1);
            list.Insert(3, 2);
            list.PrintList();
            Console.WriteLine();

            // inserting - in middle
            list.Insert(1, 1);
            list.PrintList();
            Console.WriteLine();

            // deleting
            list.Delete(2);
            list.PrintList();
            Console.WriteLine();

            // inserting at invalid positions
            try { list.Insert(-1, -1); } catch (Exception ex) { Console.WriteLine(ex.Message); }
            try { list.Insert(2000, 2000); } catch (Exception ex) { Console.WriteLine(ex.Message); }
            list.PrintList();
            Console.WriteLine();

            // deleting from invalid positions
            try { list.Delete(-1); } catch (Exception ex) { Console.WriteLine(ex.Message); }
            try { list.Delete(2000); } catch (Exception ex) { Console.WriteLine(ex.Message); }
            list.PrintList();
            Console.WriteLine();

            Console.ReadLine();
        }
    }
}
