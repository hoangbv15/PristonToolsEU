using System.Collections.ObjectModel;

namespace PristonToolsEU;

public static class ObservableCollectionExtensions
{
    public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
    {
        List<T> sorted = collection.OrderBy(x => x).ToList();
        MoveItemsInPlace(collection, sorted);
    }

    public static void SortByFavourite(this ObservableCollection<BossTimeViewModel> collection)
    {
        // Minus sign here is so that the favourites will end up at the top of the list
        List<BossTimeViewModel> sorted = collection.OrderBy(x => -x.Favourite).ToList();
        MoveItemsInPlace(collection, sorted);
    }

    private static void MoveItemsInPlace<T>(ObservableCollection<T> collection, List<T> sorted)
    {
        for (int i = 0; i < sorted.Count(); i++)
        {
            collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}