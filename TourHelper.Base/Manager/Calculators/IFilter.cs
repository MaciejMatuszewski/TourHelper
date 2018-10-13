
namespace TourHelper.Base.Manager.Calculators
{
    public interface IFilter<T>
    {
        T GetValue(T v);
    }
}
