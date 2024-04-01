using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PristonToolsEU
{
    public class FastObservableCollection<T>: ObservableCollection<T> 
    {
        public FastObservableCollection(): base() { }
        public FastObservableCollection(List<T> list): base(list)
        {
        }

        public void Rearrange(T[] sorted)
        {
            for (int i = 0; i < sorted.Length; i++)
            {
                var index1 = Items.IndexOf(sorted[i]);
                var temp = Items[index1];
                Items[index1] = Items[i];
                Items[i] = temp;
            }
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    
    }
}
