using ItemClassLibrary;
using System.Diagnostics.CodeAnalysis;

namespace lab
{
    public class Program
    {
        [ExcludeFromCodeCoverage] //создает случайный элемент
        public static Item CreateRandomItem()
        {
            Random rnd = new Random();
            Item item = new Item();
            switch (rnd.Next(0, 4))
            {
                case 1:
                    item = new Product();
                    break;
                case 2:
                    item = new DairyProduct();
                    break;
                case 3:
                    item = new Toy();
                    break;
            }
            item.RandomInit();
            return item;
        }

        [ExcludeFromCodeCoverage] // демонстрационная программа
        static void Main(string[] args)
        {
            NewHashTable<Item> t1 = new NewHashTable<Item>("Коллекция 1");
            NewHashTable<Item> t2 = new NewHashTable<Item>("Коллекция 2");
            Journal j1 = new Journal();
            Journal j2 = new Journal();
            t1.CollectionCountChanged += j1.WriteCollectionChange;
            t1.CollectionReferenceChanged += j1.WriteCollectionChange;

            t1.CollectionReferenceChanged += j2.WriteCollectionChange;
            t2.CollectionReferenceChanged += j2.WriteCollectionChange;

            t1.Insert(22, CreateRandomItem());
            t1.Add(CreateRandomItem());
            t1.Add(CreateRandomItem());
            t1[22] = CreateRandomItem();
            t1.RemoveAt(22);

            t2.Insert(32, CreateRandomItem());
            t2.Insert(45, CreateRandomItem());
            t2.Add(CreateRandomItem());
            t2[32] = CreateRandomItem();
            t2[45] = CreateRandomItem();

            Console.WriteLine("Журнал 1 с подпиской на CollectionCountChanged и CollectionReferenceChanged из Коллекции 1\n");
            j1.Show();
            Console.WriteLine("\n\n\n\nЖурнал 2 с подпиской на CollectionReferenceChanged из Коллекции 1 и из Коллекции 2\n");
            j2.Show();
        }
    }
}