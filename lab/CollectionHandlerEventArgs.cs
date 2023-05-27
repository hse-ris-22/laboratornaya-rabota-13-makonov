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
        public object? Obj { get; set; }

        public CollectionHandlerEventArgs(string collectionName, string changeType, object obj)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            Obj = obj;
        }
    }
}
