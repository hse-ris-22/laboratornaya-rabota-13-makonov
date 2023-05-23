using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string? CollectionName { get; set; }
        public string? ChangeType { get; set; }
        public object? ElementReference { get; set; }

        public CollectionHandlerEventArgs(string collectionName, string changeType, object reference)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ElementReference = reference;
        }
    }
}
