using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TestEka
{
    class Program
    {
        const int NumberOfTests = 10; // Количество генерируемых множеств для тестирования
        const int N = 10; // Длина исходного массива
        const int M = 5; // Первые М элементов
        static Random rnd = new Random();
        
        static void Main(string[] args)
        {
            for (int i = 0; i < NumberOfTests; i++) {
                int[] numbers = RandomNumbers(N);
                
                Console.WriteLine($"Массив: {String.Join(", ", numbers)}");
                Console.WriteLine($"Первые M (M = {M}) элементов содержат ноль: {ContainsZero(numbers)}");
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        /// <summary>
        /// Определяет, содержат ли первые М элементов массива хотя бы один 0
        /// </summary>
        /// <param name="array">Исходный массив длины N >= M</param>
        /// <returns></returns>
        static string ContainsZero(int[] array) => Recur_OverLimit(array, 0);

        // Массивы функций используются для ветвления
        static Func<int[], int, string>[] CheckZero = { Recur_ZeroFound, Recur_OverLimit };
        static Func<int[], int, string>[] CheckLimit = { Recur_IsZero, Recur_Finish };

        // Следующие четыре метода - общее тело рекурсии
        static string Recur_IsZero(int[] array, int i) => CheckZero[(int) (array[i] / (array[i] - 0.1) + 0.1)](array, ++i);

        static string Recur_OverLimit(int[] array, int i) => CheckLimit[i / M](array, i);
        
        // Два метода - выходы из рекурсии
        static string Recur_Finish(int[] array, int i) => "Нет";
        static string Recur_ZeroFound(int[] array, int i) => $"Да (позиция {i})";

        static int[] RandomNumbers(int N)
        {
            int[] result = new int[N];
            for (int i = 0; i < N; i++) {
                result[i] = rnd.Next(-4, 4);
            }
            return result;
        }

    }
}
