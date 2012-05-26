
namespace ProSnap.ActionItems
{
    public interface IActionItem
    {
        ActionTypes ActionType { get; }
        IActionItem Clone();
    }
}