using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Text.Json.Serialization;
using System.IO;

namespace Lab16
{
    class Program
    {
        class Product
        {
            public int Code { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }
        static void Main(string[] args)
        {
            string ProductName = "Products.json";

            Console.Write("Введите количество товаров: ");
            int n = Convert.ToInt32(Console.ReadLine());
            Product[] продукт = new Product[n];
            double Price = 0;
            int maxPrice = 0;

            for (int х = 0; х < продукт.Length; х++)
            {
                Console.WriteLine("Введите данные товара №{0}: ", х+1);
                продукт[х] = new Product();
                Console.Write("Название товара: ");
                продукт[х].Name = Console.ReadLine();
                Console.Write("Код товара: ");
                продукт[х].Code = Convert.ToInt32(Console.ReadLine());
                Console.Write("Цена товара: ");
                продукт[х].Price = Convert.ToDouble(Console.ReadLine());
            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
            };


            if (!File.Exists(ProductName))
            {
                File.Create(ProductName).Close();
            }

            using (StreamWriter streamWriter = new StreamWriter(ProductName))
            {
                for (int x = 0; x < продукт.Length; x++)
                {
                    string jsonProducts = JsonSerializer.Serialize(продукт[x], options);
                    streamWriter.WriteLine(jsonProducts);
                }
            }
            using (StreamReader streamReader = new StreamReader(ProductName))
            {
                for (int x = 0; x < продукт.Length; x++)
                {
                    продукт[x] = new Product();
                    string jsonProducts = streamReader.ReadLine();
                    продукт[x] = JsonSerializer.Deserialize<Product>(jsonProducts);
                    if (продукт[x].Price > Price)
                    {
                        Price = продукт[x].Price;
                        maxPrice = x;
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine("Самый дорогой товар: {0}.", продукт[maxPrice].Name);
            Console.ReadKey();
        }
    }

}