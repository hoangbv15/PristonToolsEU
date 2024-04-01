using Microsoft.VisualBasic;
using System;
using System.Collections;
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
        public FastObservableCollection()
        { }
        public FastObservableCollection(List<T> list): base(list)
        { }

        public void Rearrange(T[] sorted)
        {
            for (int i = 0; i < sorted.Length; i++)
            {
                var oldIndex = Items.IndexOf(sorted[i]);
                (Items[oldIndex], Items[i]) = (Items[i], Items[oldIndex]);
            }

            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    
    }
}
