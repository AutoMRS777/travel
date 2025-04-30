using System;
using System.Collections.Generic;
using G11_Mahmudjon_Mirqobilov;


namespace G11_Mahmudjon_Mirqobilov
{
    class Program
    {
        static string filePath = @"D:\travel.json";
        static TravelPlanService service = new TravelPlanService(filePath);

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n--- Sayohat Rejalari ---");
                Console.WriteLine("1. Barcha rejalani ko'rish");
                Console.WriteLine("2. Yangi reja qo'shish");
                Console.WriteLine("3. Rejani tahrirlash");
                Console.WriteLine("4. Rejani o'chirish");
                Console.WriteLine("5. ID orqali reja topish");
                Console.WriteLine("0. Chiqish");
                Console.Write("Tanlang: ");

                string choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        ShowAllPlans();
                        break;

                    case "2":
                        AddNewPlan();
                        break;

                    case "3":
                        UpPlan();
                        break;

                    case "4":
                        DeletePlan();
                        break;

                    case "5":
                        GetById();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Noto‘g‘ri tanlov.");
                        break;
                }
            }
        }

        static void ShowAllPlans()
        {
            var allPlans = service.GetAll();
            foreach (var plan in allPlans)
            {
                Console.WriteLine($"{plan.Id}: {plan.Destination} - {plan.Date.ToShortDateString()} - {plan.Note}");
            }
        }

        static void AddNewPlan()
        {
            Console.Write("Boriladigan joy: ");
            string destination = Console.ReadLine();

            Console.Write("Sana (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Noto‘g‘ri sana formati.");
                return;
            }

            Console.Write("Izoh: ");
            string note = Console.ReadLine();

        }

        static void UpPlan()
        {
            Console.Write("Tahrirlanadigan rejaning ID sini kiriting: ");
            if (!int.TryParse(Console.ReadLine(), out int editId))
            {
                Console.WriteLine("Noto‘g‘ri ID.");
                return;
            }

            var existingPlan = service.GetById(editId);
            if (existingPlan == null)
            {
                Console.WriteLine("Bunday ID mavjud emas.");
                return;
            }

            Console.Write("Yangi joy: ");
            existingPlan.Destination = Console.ReadLine();

            Console.Write("Yangi sana (yyyy-mm-dd): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime newDate))
            {
                Console.WriteLine("Noto‘g‘ri sana formati.");
                return;
            }

            existingPlan.Date = newDate;

            Console.Write("Yangi izoh: ");
            existingPlan.Note = Console.ReadLine();

            service.Update(editId, existingPlan);
            Console.WriteLine("Reja yangilandi.");
        }

        static void DeletePlan()
        {
            Console.Write("O‘chirish uchun ID ni kiriting: ");
            if (!int.TryParse(Console.ReadLine(), out int deleteId))
            {
                Console.WriteLine("Noto‘g‘ri ID.");
                return;
            }

            service.Delete(deleteId);
            Console.WriteLine("Reja o‘chirildi.");
        }

        static void GetById()
        {
            Console.Write("ID ni kiriting: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Noto‘g‘ri ID.");
                return;
            }

            var plan = service.GetById(id);
            if (plan != null)
            {
                Console.WriteLine($"{plan.Id}: {plan.Destination} - {plan.Date.ToShortDateString()} - {plan.Note}");
            }
            else
            {
                Console.WriteLine("Reja topilmadi.");
            }
        }
    }
}
