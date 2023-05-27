using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class Journal : IEnumerable<JournalEntry>
    {
        private List<JournalEntry> journal;

        public int Count { get; private set; }

        public Journal()
        {
            journal = new List<JournalEntry>();
            Count = 0;
        }

        /// <summary>
        /// Записывает в журнал новое вхождение при возникновении события смены кол-ва элементов в коллекции
        /// </summary>
        public void WriteCollectionCountChange(object source, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.CollectionName, e.ChangeType, e.Obj.ToString());
            journal.Add(je);
            Count++;
        }

        /// <summary>
        /// Записывает в журнал новое вхождение при возникновении события смены ссылки на элемент в коллекции
        /// </summary>
        public void WriteCollectionReferenceChange(object source, CollectionHandlerEventArgs e)
        {
            JournalEntry je = new JournalEntry(e.CollectionName, e.ChangeType, e.Obj.ToString());
            journal.Add(je);
            Count++;
        }


        /// <summary>
        /// Печатает в консоль все вхождения журнала
        /// </summary>
        [ExcludeFromCodeCoverage] // метод для вывода журнала
        public void Show()
        {
            if (Count == 0)
            {
                Console.WriteLine("Журнал пуст");
            }
            else
            {
                foreach (JournalEntry je in journal)
                {
                    Console.WriteLine(je);
                }
            }
        }

        public IEnumerator<JournalEntry> GetEnumerator()
        {
            foreach (JournalEntry entry in journal)
            {
                yield return entry;
            }
        }

        [ExcludeFromCodeCoverage] // данный метод не вызывается, и сооответсвенно он не реализован
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
