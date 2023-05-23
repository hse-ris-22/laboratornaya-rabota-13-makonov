using lab;
using ItemClassLibrary;
using MyCollectionLibrary;
using System.Collections;

namespace lab.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CollectionCountChangedTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>("Коллекция 1");
            Journal journal = new Journal();
            table.CollectionCountChanged += journal.WriteCollectionChange;
            table.Add(Program.CreateRandomItem());
            table.Add(Program.CreateRandomItem());
            Assert.AreEqual(2, journal.Count);
        }

        [TestMethod]
        public void CollectionReferenceChangedTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>("Коллекция 1");
            Journal journal = new Journal();
            table.CollectionReferenceChanged += journal.WriteCollectionChange;
            table.Insert(22, Program.CreateRandomItem());
            table[22] = Program.CreateRandomItem();
            Assert.AreEqual(1, journal.Count);
        }

        [TestMethod]
        public void JournalEnumerationTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>("Коллекция 1");
            Journal journal = new Journal();
            table.CollectionCountChanged += journal.WriteCollectionChange;
            table.Insert(22, Program.CreateRandomItem());
            table.Insert(32, Program.CreateRandomItem());
            int count = 0;
            foreach(JournalEntry je in journal)
            {
                count++;
            }
            Assert.AreEqual(2, journal.Count);
        }

        [TestMethod]
        public void JournalEntryOutputTest()
        {
            JournalEntry je = new JournalEntry("col1", "Add", "2");
            string actual = je.ToString();
            string expected = $"col1 Add : '2'";
            Assert.AreEqual(expected, actual);
        }


        #region Дублирование старых тестов удаления-добавления для NewHashTable
        [TestMethod]
        public void NewHashTableConstructorTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>("Хеш-таблица 1", 10);
            Assert.IsTrue(table.Name == "Хеш-таблица 1" && table.Capacity == 10);
        }

        [TestMethod]
        public void AddingNullToHashTableTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>("Хеш-таблица 1", 10);
            Assert.ThrowsException<ArgumentNullException>(() => table.Add((Item)null));
        }

        [TestMethod]
        public void AddingToHashTableTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>();
            table.Add(new Item("Линейка", 70, 289));
            table.Add(new Item("Ручка", 200, 299));
            table.Add(new Item("Карандаш", 100, 290));
            Assert.AreEqual(table.Count, 3);
        }

        [TestMethod]
        public void AddingToHashTableCollisionTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>(" ", 10);
            table.Add(new Toy("Конструктор", 4242, 8922, "Дерево", 93));
            table.Add(new Toy("Конструктор", 2853, 4511, "Мех и пух", 13));
            // хеш-код у данных элементов выходит одинаковым, происходит коллизия
            table.Display();
            Assert.AreEqual(2, table.Count);
        }

        [TestMethod]
        public void AddingToFullHashTableTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>(" ", 5);
            for (int i = 0; i < 6; i++)
            {
                Item item = new Item();
                item.RandomInit();
                table.Add(item);
            }
            Assert.AreEqual(10, table.Capacity);
        }

        [TestMethod]
        public void InsertingElementsWithIdenticalKeysToHashTableTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>(" ", 10);
            table.Insert(22, new Item("Линейка", 70, 289));
            table.Insert(22, new Item("Линейка", 71, 288));
            table.Insert(22, new Item("Линейка", 72, 287));
            // у данных элементов выходят одинаковые ключи
            Assert.AreEqual(1, table.Count);
        }

        [TestMethod]
        public void RemovingHashTableElementByValueTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>(" ", 10);
            Item item = new Item("Линейка", 70, 289);
            table.Add(item);
            bool isRemoved = table.Remove(item);
            Assert.IsTrue(isRemoved);
        }

        [TestMethod]
        public void RemovingNullFromHashTable()
        {
            NewHashTable<Item> table = new NewHashTable<Item>();
            Assert.ThrowsException<ArgumentNullException>(() => table.Remove((Item)null));
        }

        [TestMethod]
        public void RemovingNonExistingElementByValueFromHashTable()
        {
            NewHashTable<Item> table = new NewHashTable<Item>();
            bool isFound = table.Remove(new Item());
            Assert.IsFalse(isFound);
        }

        [TestMethod]
        public void RemovingHashTableElementByKeyTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>(" ", 10);
            Item item = new Item("Линейка", 70, 289);
            table.Insert(22, item);
            table.RemoveAt(22);
            Assert.AreEqual(0, table.Count);
        }

        [TestMethod]
        public void RemovingNonExistingElementByKeyFromHashTableTest()
        {
            NewHashTable<Item> table = new NewHashTable<Item>(" ", 10);
            table.Insert(22, new Item());
            table.RemoveAt(15);
            Assert.AreEqual(1, table.Count);
        }

        #endregion

    }
}