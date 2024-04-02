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

    private static FastObservableCollection<T> MoveItemsInline<T>(FastObservableCollection<T> collection, T[] sorted,
        CancellationToken token, SynchronizationContext sctx)
    {
        SynchronizationContext.SetSynchronizationContext(sctx);
        sctx.Post(state =>
        {
            collection.Rearrange(sorted);
        }, null);
        return collection;
    }
}