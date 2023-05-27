using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCollectionLibrary;

namespace lab
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

    public class NewHashTable<T> : HashTable<T> where T: IComparable<T>, ICloneable
    {
        public string Name { get; set; }

        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        public NewHashTable(string name = "", int capacity = 8) : base(capacity)
        {
            Name = name;
        }
                
        //Отвечает за обработку события CollectionCountChanged
        public void OnCountChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionCountChanged?.Invoke(source, args);
        }

        //Отвечает за обработку события CollectionReferenceChanged
        public void OnReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            CollectionReferenceChanged?.Invoke(source, args);
        }

        //Добавлено возникновение события CollectionCountChanged при добавлении нового элемента
        //и возникновение события CollectionReferenceChanged при изменении ссылки элемента
        //P.S. данный метод вызывается как при добавлении через Add и Insert, так и в индексаторе
        protected override void AddHashTableElement(HashTableElement<T> element)
        {
            if (element.Value is null)
                throw new ArgumentNullException();

            if ((Convert.ToDouble(Count) / Convert.ToDouble(Capacity) * 100) >= 75)
                IncreaseCapacityAndRehash();

            int hash = GetHash(element.Key);
            while (table[hash] is not null && table[hash].Key != element.Key)
            {
                hash = (hash + 1) % Capacity;
            }

            if (table[hash] is null || (table[hash] is not null && table[hash].Key != element.Key))
            {
                Count++;
                OnCountChanged(this, new CollectionHandlerEventArgs(Name, "добавление", element));
            }
            else if (table[hash] is not null && table[hash].Key == element.Key)
            {
                OnReferenceChanged(this, new CollectionHandlerEventArgs(Name, "изменение ссылки", element));
            }
            table[hash] = element;
        }

        //Добавлено возникновение события CollectionCountChanged при удалении элемента по значению
        public override bool Remove(T data)
        {
            if (data is null)
                throw new ArgumentNullException();

            for (int i = 0; i < Capacity; i++)
            {
                if (table[i] is not null && table[i].Value.Equals(data))
                {
                    OnCountChanged(this, new CollectionHandlerEventArgs(Name, "удаление по значению", table[i]));
                    table[i] = null;
                    Count--;
                    return true;
                }
            }
            return false;
        }

        //Добавлено возникновение события CollectionCountChanged при удалении элемента по ключу
        public override void RemoveAt(int key)
        {
            int hash = GetHash(key);
            int tried = 1;
            while (tried <= Capacity)
            {
                if (table[hash] is not null && table[hash].Key == key)
                {
                    OnCountChanged(this, new CollectionHandlerEventArgs(Name, "удаление по ключу", table[hash]));
                    table[hash] = null;
                    Count--;
                    return;
                }
                hash = (hash + 1) % Capacity;
                tried++;
            }
        }
    }
}
