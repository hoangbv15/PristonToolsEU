using System.Collections.ObjectModel;

namespace PristonToolsEU;

public static class ObservableCollectionExtensions
{
    public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
    {
        List<T> sorted = collection.OrderBy(x => x).ToList();
        for (int i = 0; i < sorted.Count(); i++)
        {
            collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}