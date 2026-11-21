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

        // Табулювання: буде пізніше
        static void ShowCostTable()
        {
            Console.WriteLine("Поки не зроблено.");
        }

        // Таблиця виторгу: буде пізніше
        static void ShowRevenueTable()
        {
            Console.WriteLine("Поки не зроблено.");
        }
    }
}