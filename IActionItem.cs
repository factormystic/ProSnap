using System.Threading.Tasks;

namespace ProSnap.ActionItems
{
    public interface IActionItem
    {
        ActionTypes ActionType { get; }
        IActionItem Clone();

        ExtendedScreenshot Invoke(ExtendedScreenshot LatestScreenshot);
    }
}