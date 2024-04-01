using System.Collections.ObjectModel;

namespace PristonToolsEU;

public static class ObservableCollectionExtensions
{
    public static ObservableCollection<T> Sort<T>(this ObservableCollection<T> collection) where T : IComparable
    {
        var sorted = collection.OrderBy(x => x).ToArray();
        return MakeObservableCollectionCopy(sorted);
    }

    public static ObservableCollection<BossTimeViewModel> SortByFavourite(this ObservableCollection<BossTimeViewModel> collection)
    {
        // Minus sign here is so that the favourites will end up at the top of the list
        var sorted = collection.OrderBy(x => -x.Favourite).ToArray();
        return MakeObservableCollectionCopy(sorted);
    }

    private static ObservableCollection<T> MakeObservableCollectionCopy<T>(T[] sorted)
    {
        return new ObservableCollection<T>(sorted);
    }
}