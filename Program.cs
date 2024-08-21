using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;


public class Fruit
{
    private string _name;
    private string _color;
    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public string Color
    {
        get { return _color; }
        set { _color = value; }
    }

    public Fruit()
    {

    }
    public Fruit(string name, string color)
    {
        _name = name;
        _color = color;
    }

    public virtual void Input()
    {
        Console.WriteLine("Enter fruit name:");
        _name = Console.ReadLine();
        Console.WriteLine("Enter color fruit:");
        _color = Console.ReadLine();
    }

    public virtual void Output()
    {
        Console.WriteLine($"Fruit {_name}, Color {_color}");
    }

    public override string ToString()
    {
        return $"Fruit {_name}, Color {_color}";
    }
}

public class Citrus : Fruit
{
    private double _vitaminCGram;

    public double VitaminCGram
    {
        get { return (double)_vitaminCGram; }
        set { _vitaminCGram = value; }
    }
    public Citrus()
    {
    }

    public Citrus(string name, string color, double vitaminCGram)
        :base(name, color)
    {
        _vitaminCGram = vitaminCGram;
    }

    public override void Input()
    {
        base.Input();
        Console.WriteLine("Enter Vitamin C content (in grams):");
        _vitaminCGram = Convert.ToDouble(Console.ReadLine());
    }

    public override void Output()
    {
        base.Output();
        Console.WriteLine($"Citrus: {Name}, Color: {Color}, Vitamin C Content: {_vitaminCGram}g");
    }

    public override string ToString()
    {
        return $"Citrus: {Name}, Color: {Color}, Vitamin C Content: {_vitaminCGram}g";
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        List<Fruit> list = new List<Fruit>() {
        new Citrus("Apple", "Red", 3.1),
        new Citrus("Grapes", "Green", 1.5),
        new Citrus("Lemon", "Yellow", 3.5),
        new Citrus("Banana", "Yellow", 5.5),
        new Citrus("Orange", "Orange", 4.1),
        new Citrus("Lemon", "Yellow", 2.9)
        };
        Console.WriteLine("Yellow fruicts:");
        foreach (Fruit f in list)
        {
            if(f.Color == "Yellow")
            {
                f.Output();
            }
        }

        list.Sort((x, y)=>x.Name.CompareTo(y.Name));
        string files = "sort.txt";
        try
        {
            using (StreamWriter sw = new StreamWriter(files))
            {
                foreach (Fruit f in list)
                {
                    sw.WriteLine(f.ToString());
                }
            }
            if (File.Exists(files))
            {
                Console.WriteLine($"Sorted fruit list has been successfully written to {files}");
            }
            else
            {
                Console.WriteLine(":(");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while writing the file: {ex.Message}");
        }



    }
    
    public static void SerializeToXml(List<Fruit> list, string file)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Fruit>));
        using (FileStream fs = new FileStream(file, FileMode.Create))
        {
            serializer.Serialize(fs, list);
        }
    }

    public static List<Fruit> DeserializeFromXml(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Fruit>));
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            return (List<Fruit>)serializer.Deserialize(fs);
        }
    }

    public static void SerializeToJson(List<Fruit> fruits, string filePath)
    {
        string json = JsonConvert.SerializeObject(fruits, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static List<Fruit> DeserializeFromJson(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Fruit>>(json);
    }
}

