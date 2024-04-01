using System.Collections.ObjectModel;

namespace PristonToolsEU;

public static class ObservableCollectionExtensions
{
    public static void Sort<T>(this FastObservableCollection<T> collection, CancellationToken token, SynchronizationContext sctx) where T : IComparable
    {
        var sorted = collection.OrderBy(x => x).ToArray();
        MoveItemsInline(collection, sorted, token, sctx);
    }

    public static void SortByFavourite(this FastObservableCollection<BossTimeViewModel> collection, CancellationToken token, SynchronizationContext sctx)
    {
        // Minus sign here is so that the favourites will end up at the top of the list
        var sorted = collection.OrderByDescending(x => x.Favourite).ToArray();
        MoveItemsInline(collection, sorted, token, sctx);
    }

    //public static FastObservableCollection<T> MakeObservableCollectionCopy<T>(
    //    FastObservableCollection<T> collection, T[] sorted)
    //{
    //    return new FastObservableCollection<T>(sorted);
    //}

    private static FastObservableCollection<T> MoveItemsInline<T>(FastObservableCollection<T> collection, T[] sorted,
        CancellationToken token, SynchronizationContext sctx)
    {
        SynchronizationContext.SetSynchronizationContext(sctx);
        sctx.Post(state =>
        {
            collection.Rearrange(sorted);
            //for (int i = 0; i < sorted.Count(); i++)
            //{
            //    token.ThrowIfCancellationRequested();
            //    //sctx.Post(state => { collection.Move(collection.IndexOf(sorted[i]), i); }, null);
            //    collection.Move(collection.IndexOf(sorted[i]), i);
            //}
        }, null);
        return collection;
    }

    public static void Sort1<T>(this ObservableCollection<T> array, CancellationToken token) where T: IComparable
    {
        QuickSort(array, 0, array.Count - 1,
            (a, b) => a.CompareTo(b), token);
    }
    
    
    public static void SortByFavourite1(this ObservableCollection<BossTimeViewModel> array, CancellationToken token)
    {
        QuickSort<BossTimeViewModel>(array, 0, array.Count - 1,
            (a, b) => b.Favourite - a.Favourite, token);
    }

    private static async Task QuickSort<T>(ObservableCollection<T> array, int leftIndex, int rightIndex,
        Func<T, T, int> comparer, CancellationToken token)
    {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                token.ThrowIfCancellationRequested();
                while (comparer(array[i], pivot) < 0)
                {
                    token.ThrowIfCancellationRequested();
                    i++;
                }
        
                while (comparer(array[j], pivot) > 0)
                {
                    token.ThrowIfCancellationRequested();
                    j--;
                }
                if (i <= j)
                {
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }
    
            if (leftIndex < j)
                await QuickSort(array, leftIndex, j, comparer, token);
            if (i < rightIndex)
                await QuickSort(array, i, rightIndex, comparer, token);
            // return array;
    } 
}