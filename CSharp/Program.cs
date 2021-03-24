using System;
using static System.Console;

namespace CSharp
{
    class Data {
        public int value;
        public Data() {
            value = 30;
        }
    }
    class Program
    {
        enum Day { Sat, Sun, Mon, Tue, Wed, Thu, Fri};

        static void Swap (ref int a, ref int b) {
            int temp = b;
            b = a;
            a = temp;
        }

        static void Move (int a, ref int b) {
            b = a;
        }

        static void MoveOut (int a, out int b) {
            b = a;
        }

        static int Sum (params int [] args) {
            int sum = 0;
            for (int i = 0; i < args.Length; i ++) {
                sum += args[i];
            }
            return sum;
        }

        static float Sum (params float [] args) {
            float sum = 0;
            for (int i = 0; i < args.Length; i ++) {
                sum += args[i];
            }
            return sum;
        }

        static void DefaultParam(int a, int b = 0, int c = 0) {
            Console.WriteLine($"a = {a} b = {b} c = {c}");
        }

        static void Main(string[] args)
        {
            WriteLine("Hello World!");

            if (args.Length == 0) {
                WriteLine("Args == 0");
                return;
            }

            WriteLine($"Args.Length == {args.Length} args[0] = {args[0]}");

            bool b = true;

            WriteLine($"b = {b}");

            Day d = Day.Sun;

            WriteLine($"d = {d}");

            string v1 = "Dot", v2 = "Net";

            WriteLine("v1 = {0} v2 = {1}", v1, v2);
            WriteLine($"v1 = {v1} v2 = {v2}");

            int? k = null;
            WriteLine($"k ?? 1 = {k ?? 1} k.HasValue = {k.HasValue}");

            Data data = new Data();
            WriteLine($"d.value = {data.value}");

            Data? da = null;
            int? v = da?.value;
            WriteLine($"v = {v}");

            da = new Data();
            v = da?.value;
            WriteLine($"v = {v}");

            int[] arr = null;
            v = arr?[0];

            if (v == null) {
                WriteLine("v = k?[0] is null");
            }

            string str = null;
            WriteLine($"str = {str ?? "Default"}");
            str = "Hello";
            WriteLine($"str = {str ?? "Default"}");

            int bigInt = 10_000_000;
            int hexInt = 0xff_f0;
            int binInt = 0b0_001_1010_0011;

            int swapA = 20, swapB = 30;
            Swap(ref swapA, ref swapB);
            WriteLine($"swapA = {swapA} swapB = {swapB}");

            Move(swapA, ref swapB);
            WriteLine($"swapA = {swapA} swapB = {swapB}");

            swapA = 20; swapB = 30;
            Move(swapA, ref swapB);
            WriteLine($"swapA = {swapA} swapB = {swapB}");

            swapA = 20; swapB = 30;
            MoveOut(swapA, out swapB);
            WriteLine($"swapA = {swapA} swapB = {swapB}");

            WriteLine($"Sum = {Sum(1, 2, 3, 4, 5)}");
            WriteLine($"Sum = {Sum(1.1f, 2.2f, 3.3f, 4.4f, 5.5f)}");

            DefaultParam(a: 2, c: 3);
        }
    }
}
