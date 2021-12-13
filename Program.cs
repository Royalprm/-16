using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Text.Unicode;
using System.Text.Encodings.Web;

namespace Упражнение_16

// 1. Необходимо разработать программу для записи информации о товаре в текстовый файл в формате json.
//Разработать класс для моделирования объекта «Товар». Предусмотреть члены класса «Код товара» (целое число), «Название товара» (строка), «Цена товара» (вещественное число).
//Создать массив из 5-ти товаров, значения должны вводиться пользователем с клавиатуры.
//Сериализовать массив в json-строку, сохранить ее программно в файл «Products.json».

// 2. Необходимо разработать программу для получения информации о товаре из json-файла.
// Десериализовать файл «Products.json» из задачи 1. Определить название самого дорогого товара.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 5; 
            int n = 0; 
            Item[] itemContaiter = new Item[i];

            
            while (n != i)
            {
                Console.WriteLine($"Товар {++n} ");
                Console.Write($"Введите код товара: ");
                int code = Convert.ToInt32(Console.ReadLine());
                Console.Write($"Введите наименование товара: ");
                string name = Console.ReadLine();
                Console.Write($"Введите цену товара: ");
                double price = Convert.ToDouble(Console.ReadLine());
                itemContaiter[n - 1] = new Item(code, name, price);
                Console.WriteLine("Товар занесен в базу данных");
                Console.ReadKey();
                Console.Clear();
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic);
            jso.WriteIndented = true;

            
            string path = @"D:\Items\Items.json"; 
            if (!File.Exists(path)) 
            {
                try
                {
                    File.Create(path).Close();
                    
                }
                catch (AccessViolationException)
                {
                    Console.WriteLine($"Нет доступа {path}!");
                }
            }
            using (StreamWriter sw = new StreamWriter(path)) 
            {
                foreach (Item item in itemContaiter)
                {
                    sw.WriteLine(JsonSerializer.Serialize(item));
                    Console.WriteLine("Файл записан");
                }
            }

            

            Item mostExpencive = new Item(); 
            string path_1 = @"D:\Items\Items.json"; 
            if (File.Exists(path_1))
            {
                
                using (StreamReader sr = new StreamReader(path_1))
                {
                    string line = sr.ReadLine();
                    while (line != null) 
                    {
                        Item j = JsonSerializer.Deserialize<Item>(line);
                        mostExpencive = (j.Price > mostExpencive.Price) ? j : mostExpencive; 
                        line = sr.ReadLine(); 
                    }
                }
            }
            else
            {
                throw new FileNotFoundException($"Файл {path} не найден!");
            }

            
            Console.WriteLine($"Самый дорогой товар: {mostExpencive.Name}");
            Console.WriteLine("\nНажмите любую кнопку.");
            Console.ReadKey();
        }
    }
    public class Item
    {
        public Item() 
        {
            Code = 0;
            Name = "";
            Price = 0;
        }
        public Item(int code, string name, double price)
        {
            Code = code;
            Name = name;
            Price = price;
        }

        public int Code { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
                 
    
    

