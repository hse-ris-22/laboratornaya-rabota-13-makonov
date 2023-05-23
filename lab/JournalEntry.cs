using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ObjectData { get; set; }

        public JournalEntry(string collectionName, string changeType, string objectData)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ObjectData = objectData;
        }

        public override string ToString()
        {
            return $"{CollectionName} {ChangeType} : '{ObjectData}'";
        }
    }
}
