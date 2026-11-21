using System;
using System.Text;

namespace TaxiService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Щоб українські літери коректно виводилися в консолі
            Console.OutputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("=== Служба таксі ===");
                Console.WriteLine("1 — довідка про програму");
                Console.WriteLine("2 — вартість поїздки залежно від відстані");
                Console.WriteLine("3 — виторг водіїв по днях");
                Console.WriteLine("0 — вихід");
                Console.Write("Ваш вибір: ");

                // Читаємо рядок, а не int: Enter чи літера не валять програму
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        ShowCostTable();
                        Pause();
                        break;
                    case "3":
                        ShowRevenueTable();
                        Pause();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("До побачення.");
                        break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        // Пауза й очищення екрана після кожної дії
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            Console.ReadKey(true);
            Console.Clear();
        }

        // Довідка: тема, сутність, поля з типами
        static void ShowAbout()
        {
            Console.WriteLine("Тема: Служба таксі (№13).");
            Console.WriteLine("Головна сутність: Поїздка. Полів: 8.");
            Console.WriteLine("Довідник: Спосіб оплати.");
            Console.WriteLine();
            Console.WriteLine("Поля сутності «Поїздка»:");
            Console.WriteLine("  int      — номер (призначає програма)");
            Console.WriteLine("  string   — водій");
            Console.WriteLine("  DateTime — дата й час");
            Console.WriteLine("  string   — адреса подачі");
            Console.WriteLine("  string   — адреса призначення");
            Console.WriteLine("  double   — відстань, км");
            Console.WriteLine("  double   — тариф за км");
            Console.WriteLine("  int      — код способу оплати (з довідника)");
            Console.WriteLine();
            Console.WriteLine("Підсумкова величина: виторг по кожному водію за зміну.");
        }

        // Табулювання: вартість поїздки залежно від відстані
        static void ShowCostTable()
        {
            double from = 1.0;      // початок, км
            double to = 15.0;       // кінець, км
            double step = 1.5;      // дробовий крок
            double boarding = 50.0; // вартість посадки, грн
            double tariff = 18.0;   // тариф за км, грн
            double nightRate = 1.25; // нічний коефіцієнт

            Console.WriteLine("Вартість поїздки залежно від відстані");
            Console.WriteLine($"{"Км",8}{"День, грн",14}{"Ніч, грн",14}");
            Console.WriteLine(new string('-', 36));

            // + 1e-9, щоб остання стрічка не загубилася через похибку double
            for (double x = from; x <= to + 1e-9; x += step)
            {
                double day = boarding + tariff * x;
                double night = day * nightRate;
                Console.WriteLine($"{x,8:F2}{day,14:F2}{night,14:F2}");
            }
        }

         // Читає додатне ціле в діапазоні 1..max; false — якщо ввід неправильний
        static bool ReadPositive(string prompt, int max, out int value)
        {
            Console.Write(prompt);
            string text = Console.ReadLine();
            if (!int.TryParse(text, out value) || value <= 0 || value > max)
            {
                Console.WriteLine($"Помилка: потрібне ціле число від 1 до {max}.");
                return false;
            }
            return true;
        }

        // Таблиця на два входи: рядки — водії, стовпці — дні, підсумки по краях
        static void ShowRevenueTable()
        {
            int rows, cols;

            // Розміри питаємо в користувача; при помилці нічого не малюємо
            if (!ReadPositive("Кількість водіїв (рядків), до 20: ", 20, out rows)) return;
            if (!ReadPositive("Кількість днів (стовпців), до 10: ", 10, out cols)) return;

            // Сума по кожному стовпцю (масив фіксованої довжини)
            int[] colSums = new int[cols];
            int grandTotal = 0;

            Console.WriteLine();
            Console.WriteLine("Виторг водіїв по днях, грн");

            // Шапка: пропуск на ширину стовпця з назвами водіїв
            Console.Write($"{"",10}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{"День " + col,8}");
            }
            Console.WriteLine($"{"Разом",10}");

            // Таблиця: зовнішній цикл — рядки, внутрішній — стовпці
            for (int row = 1; row <= rows; row++)
            {
                Console.Write($"{"Водій " + row,-10}");
                int rowSum = 0; // обнуляємо для кожного нового рядка

                for (int col = 1; col <= cols; col++)
                {
                    // Даних ще немає, тому значення — умовна формула
                    int value = (row * 3 + col * 2) * 10;
                    Console.Write($"{value,8}");
                    rowSum += value;
                    colSums[col - 1] += value;
                }

                Console.WriteLine($"{rowSum,10}");
                grandTotal += rowSum;
            }

            // Рядок підсумків — тієї самої ширини, що й таблиця
            Console.Write($"{"Разом",-10}");
            for (int col = 0; col < cols; col++)
            {
                Console.Write($"{colSums[col],8}");
            }
            Console.WriteLine($"{grandTotal,10}");
        }
    }
}