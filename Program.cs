// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using project1.Data.Models;
namespace project1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            PharmacyDbContext pharmacyDbContext = new PharmacyDbContext();
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1 - Изход");
                Console.WriteLine("2 - всички лекарства");
                Console.WriteLine("3 - всички лекарства с наличност над 50");
                Console.WriteLine("4 - най-скъпите 3 лекарства в аптеката");
                Console.WriteLine("5 - всички служители");
                Console.WriteLine("6 - всички поръчки от даден доставчик");
                Console.WriteLine("7 - лекарства с изчерпано количество");
                Console.WriteLine("8 - всички имена на лекари, които са писали рецепти");
                Console.WriteLine("9 - при въвеждане на лекар да се изведат имената на пациентите, на които този лекар е изписвал лекарства");
                Console.WriteLine("10 - общата стойност на всички поръчки");

                int num = int.Parse(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        Console.WriteLine();
                        break;
                    case 2:
                        await AllMedicines(pharmacyDbContext);
                        break;
                    case 3:
                        await Allmedicine50(pharmacyDbContext);
                        break;
                    case 4:
                        await Top3Medicine(pharmacyDbContext);
                        break;
                    case 5:
                        await AllEmploeey(pharmacyDbContext);
                        break;
                    case 6:
                        await AllOrderEmployees(pharmacyDbContext);
                        break;
                    case 7:
                        await MedicineNolQuerity(pharmacyDbContext);
                        break;
                    case 8:
                        await DoctornAME(pharmacyDbContext);
                        break;
                    case 9:
                        await PacientNameDoctor(pharmacyDbContext);
                        break;
                        case 10:
                            await AllPriceOrder(pharmacyDbContext);
                        break;
                    default:
                        Console.WriteLine("Неправилна команда");
                        break;
                }

                if (num == 1)
                {
                    break;
                }
            }

        }

        public static async Task AllMedicines(PharmacyDbContext pharmacyDbContext)
        {
            var medicines = await pharmacyDbContext.Medicines.ToListAsync<Medicine>();
            foreach (var medicine in medicines)
            {
                Console.WriteLine(medicine.Name);
            }
        }

        public static async Task Allmedicine50(PharmacyDbContext pharmacyDbContext)
        {
            var medicines = await pharmacyDbContext.Medicines.Where(p => p.QuantityInStock > 50).ToListAsync<Medicine>();
            foreach (var item in medicines)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static async Task Top3Medicine(PharmacyDbContext pharmacyDbContext)
        {
            var medicine = await pharmacyDbContext.Medicines.OrderByDescending(p => p.Price).Take(3).ToListAsync();
            foreach (var item in medicine)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static async Task AllEmploeey(PharmacyDbContext pharmacyDbContext)
        {
            var employyes = await pharmacyDbContext.Employees.ToListAsync<Employee>();
            foreach (var item in employyes)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static async Task AllOrderEmployees(PharmacyDbContext pharmacyDbContext)
        {
            Console.WriteLine("Въведи име:");
            string name = Console.ReadLine();
            var oreders = await pharmacyDbContext.Orders.Where(o => o.SupplierName == name).ToListAsync<Order>();

            if (oreders.Count == 0)
            {
                Console.WriteLine("Няма!!!");
            }
            else
            {
                foreach (var o in oreders)
                {
                    Console.WriteLine($"{o.SupplierName} - {o.OrderDate} - {o.QuantityOrdered}");
                }
            }
        }

        public static async Task MedicineNolQuerity(PharmacyDbContext pharmacyDbContext)
        {
            var medicine = await pharmacyDbContext.Medicines.Where(m => m.QuantityInStock == 0).ToListAsync();
            foreach (var item in medicine)
            {
                Console.WriteLine(item.Name);
            }
        }

        public static async Task DoctornAME(PharmacyDbContext pharmacyDbContext)
        {
            var doctor = await pharmacyDbContext.Prescriptions.Select(p => p.DoctorName).ToListAsync();
            foreach (var item in doctor)
            {
                Console.WriteLine(item);
            }
        }

        public static async Task PacientNameDoctor(PharmacyDbContext pharmacyDbContext)
        {
            Console.WriteLine("Име доктор");
            string name = Console.ReadLine();
            var pacient = await pharmacyDbContext.Prescriptions.Where(p => p.DoctorName == name).ToListAsync();
            foreach (var item in pacient)
            {
                Console.WriteLine(item.PatientName);
            }
        }

        public static async Task AllPriceOrder(PharmacyDbContext pharmacyDbContext)
        {
            var order = await pharmacyDbContext.Orders.SumAsync(order => order.QuantityOrdered * pharmacyDbContext.Medicines
        .Where(medicine => medicine.IdMedicine == order.IdMedicine)
        .Select(medicine => medicine.Price)
        .FirstOrDefault());
            Console.WriteLine(order);

        }
    }

}