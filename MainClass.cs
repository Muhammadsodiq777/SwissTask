using SwissTask.Data;
using System.Text.Json;

namespace SwissTask;

public class MainClass
{
    public Truck truck = new();
    public SportsCar sports = new();
    public PassangerCars cars = new();

    public MainClass(Truck truck, SportsCar sports, PassangerCars cars)
    {
        this.truck = truck;
        this.sports = sports;
        this.cars = cars;
    }

    public MainClass()
    {
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("\t\t\tWelcome to Car program\n");

        MainClass main = new MainClass();

        int flag = 1;
        int opt;
        int carType;

        while (flag != 0)
        {
            Console.WriteLine("\tPlease select one option");
            Console.WriteLine(" -- Create new car: 1");
            Console.WriteLine(" -- See how much does car can go: 2");
            Console.WriteLine(" -- Display all the cars: 3");
            Console.WriteLine(" -- Exit the program: 4");

            opt = Convert.ToInt32(Console.ReadLine());

            switch (opt)
            {
                case 1:
                    {

                        Console.WriteLine("Please select car type");
                        Console.WriteLine("Passanger car: 1");
                        Console.WriteLine("Sports car: 2");
                        Console.WriteLine("Truck: 3");
                        carType = Convert.ToInt32(Console.ReadLine());

                        main.CreateCar(carType);

                    }
                    break;
                case 2: {
                        main.DisplayCars();


                        Console.WriteLine("Please choose car(type ID of the car): ");
                        long Id = Convert.ToInt64(Console.ReadLine());

                        Console.WriteLine("Please enter your passangers number: ");
                        int ps = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Please enter the distance: ");
                        var ds = Convert.ToInt32(Console.ReadLine());

                        main.MaximumWayCarTravel(ps, ds, Id);
                    }
                    break;
                case 3: { } break;
                case 4: { flag = 0; } break;
                default:
                    Console.WriteLine($"No option like {opt}");
                    break;
            }
        }

        Console.WriteLine("Thanks for being with us 😊");
    }

    public string CreateCar(int carType)
    {

        Console.WriteLine("Plese enter the Name");
        string Name = Console.ReadLine();

        Console.WriteLine("Plese enter the Description");
        string Description = Console.ReadLine();

        Console.WriteLine("Plese enter the Model");
        string Model = Console.ReadLine();

        Console.WriteLine("Plese enter the Type");
        string Type = Console.ReadLine();

        Console.WriteLine("Plese enter the FuelTankVolume");
        double FuelTankVolume = Convert.ToDouble(Console.ReadLine());

        Console.WriteLine("Plese enter the Speed");
        double speed = Convert.ToDouble(Console.ReadLine());

        if (carType == 1)
        {

            Console.WriteLine("Plese enter the PassangerNumber");
            int psVolume = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Plese enter the 1 litr is enough for km: ");
            int fc = Convert.ToInt32(Console.ReadLine());

            cars.Name = Name;
            cars.Description = Description;
            cars.Model = Model;
            cars.Type = Type;
            cars.FuelTankVolume=FuelTankVolume;
            cars.Speed = speed;
            cars.PassangerVolume = psVolume;
            cars.FuelConsumtion = fc;
            cars.Id = FindCarId();


            List<Car> carsList = new List<Car>();

            carsList.Add(cars);

            string jsonString = JsonSerializer.Serialize(carsList, new JsonSerializerOptions() { WriteIndented = true });
            StreamWriter PassangerJ = new StreamWriter("D:\\Passanger.json", true);
            PassangerJ.WriteLine(jsonString);
            PassangerJ.Close();
            PassangerJ.Dispose();

            Console.WriteLine($"{Name} Created successfully");
            return "";
        }
        else if(carType == 2)
        {
            StreamWriter SportsJ = new StreamWriter("D:\\Sports.json", true);
            sports.Name = Name;
            sports.Description = Description;
            sports.Model = Model;
            sports.Type = Type;
            sports.FuelTankVolume = FuelTankVolume;
            sports.Speed = speed;
            sports.Id = FindSportsId();

            string jsonString = JsonSerializer.Serialize(sports, new JsonSerializerOptions() { WriteIndented = true });
            SportsJ.WriteLine(jsonString);
            SportsJ.Close();
            SportsJ.Dispose();

            Console.WriteLine($"{Name} Created successfully");
            return "";
        }
        else if(carType == 3)
        {
            StreamWriter TruckJ = new StreamWriter("D:\\Truck.json", true);

            Console.WriteLine("Plese enter the LaguageVolume");
            int LgVolume = Convert.ToInt32(Console.ReadLine());

            truck.Name = Name;
            truck.Description = Description;
            truck.Model = Model;
            truck.Type = Type;
            truck.FuelTankVolume = FuelTankVolume;
            truck.Speed = speed;
            truck.LagueageVolume = LgVolume;

            truck.Id = FindTruckId();

            string jsonString = JsonSerializer.Serialize(truck, new JsonSerializerOptions() { WriteIndented = true });
            TruckJ.WriteLine(jsonString);
            TruckJ.Close();
            TruckJ.Dispose();

            Console.WriteLine($"{Name} Created successfully");
            return "";
        }

        Console.WriteLine($"{Name} No option like this");
        return "";
    }

    public int MaximumWayCarTravel(int Passangers, int Distance, long Id)
    {
        int Maxway = 0;

        if (CheckPassagerNumber(Passangers, Id) != null)
        {
            var currentCar = CheckPassagerNumber(Passangers, Id);

            int distancePerLitr = Convert.ToInt32(currentCar.FuelConsumtion);

            var MaxDistance = Convert.ToInt32(currentCar.FuelTankVolume) * distancePerLitr;

            double max = (MaxDistance / 100) * (Passangers * 6);
            Maxway = Convert.ToInt32(max);
            Console.WriteLine($"We have {Passangers} number of passagers and we can travel maximum {max} " +
                $" with current full tank of fuel \n" +
                $"");

            var fuelRequired = (Distance - max) / distancePerLitr;
            if (fuelRequired > 0)
                Console.WriteLine($"To arrive the asked distance we need {fuelRequired} litr more fuel");
            else
                Console.WriteLine($"We have {Math.Abs(fuelRequired)} extre fuel");
        }
        else
            Console.WriteLine($"Car is not present");

        return Maxway;
    }

    public long FindTruckId()
    {
        List<Truck> source = new List<Truck>();

        using (StreamReader r = new StreamReader("D:\\Truck.json"))
        {
            string json = r.ReadToEnd();
            if (String.IsNullOrEmpty(json))
            {
                return 1;
            }
            source = JsonSerializer.Deserialize<List<Truck>>(json);
            r.Close();
            r.Dispose();
        }

        int len = source.Count;

        var obj = source[len - 1];
        long BiggetId = obj.Id;

        return BiggetId + 1;
    }

    public long FindCarId()
    {
        List<PassangerCars> source = new List<PassangerCars>();

        FileStream f = new FileStream("D:\\Passanger.json", FileMode.OpenOrCreate);

        using (StreamReader r = new StreamReader(f))
        {
            string json = r.ReadToEnd();
            if (String.IsNullOrEmpty(json))
            {
                return 1;
            }
            source = JsonSerializer.Deserialize<List<PassangerCars>>(json);
            f.Close();
            r.Close();
            r.Dispose();
        }
        int len = source.Count;

        var obj = source[len - 1];
        long BiggetId = obj.Id;

        return BiggetId + 1;
    }

    public long FindSportsId()
    {
        List<SportsCar> source = new List<SportsCar>();

        using (StreamReader r = new StreamReader("D:\\Sports.json"))
        {
            string json = r.ReadToEnd();
            if (String.IsNullOrEmpty(json))
            {
                return 1;
            }
            source = JsonSerializer.Deserialize<List<SportsCar>>(json);
            r.Close();
            r.Dispose();


        }
        int len = source.Count;

        var obj = source[len - 1];
        long BiggetId = obj.Id;

        return BiggetId + 1;
    }
    public PassangerCars CheckPassagerNumber(int num, long id)
    {
        List<PassangerCars> source = new List<PassangerCars>();

        using (StreamReader r = new StreamReader("D:\\Passanger.json"))
        {
            string json = r.ReadToEnd();
            if (String.IsNullOrEmpty(json))
            {
                return null;
            }
            source = JsonSerializer.Deserialize<List<PassangerCars>>(json);
            r.Close();
            r.Dispose();


        }
        var obj = new PassangerCars();

        foreach (var item in source)
        {
            if(item.Id == id)
            {
                obj = item;
                break;
            }
        }

        if (obj.PassangerVolume >= num)
        {
            Console.WriteLine("Passager value is correct");
            return obj;
        }
        else
            Console.WriteLine($"You have entered wrong number of passagers{num}");
        return null;
    }

    public void DisplayCars()
    {
        List<PassangerCars> source = new List<PassangerCars>();

        using (StreamReader r = new StreamReader("D:\\Passanger.json"))
        {
            string json = r.ReadToEnd();
            if (String.IsNullOrEmpty(json))
            {
                Console.WriteLine("No cars found");
            }
            source = JsonSerializer.Deserialize<List<PassangerCars>>(json);
            r.Close();
            r.Dispose();
        }
        foreach (var item in source)
        {
            Console.WriteLine($"{item.Id} -> {item.Name}, {item.Description}," +
                $" {item.Model}, {item.Type}, {item.FuelTankVolume}, {item.Speed} \n");
        }

    }
}
