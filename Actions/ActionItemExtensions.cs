
namespace ProSnap.ActionItems
{
    public enum ActionTypes
    {
        None,

        TakeForegroundScreenshot,
        TakeRegionScreenshot,

        BeginScrollingScreenshot,
        ContinueScrollingScreenshot,
        EndScrollingScreenshot,

        ShowPreview,
        HidePreview,

        Heart,
        Save,
        Upload,
        ApplyEdits,
        Delete,

        Run,
        Clipboard,
    };

    public static class ActionItemExtensions
    {
        public static string DisplayText(this ActionTypes at)
        {
            return System.Text.RegularExpressions.Regex.Replace(at.ToString(), @"(?<!^)([A-Z])", " $1");
        }

        public static IActionItem ToInstance(this ActionTypes at)
        {
            switch(at)
            {
                case ActionTypes.TakeForegroundScreenshot: return new TakeForegroundScreenshotAction();
                case ActionTypes.TakeRegionScreenshot: return new TakeRegionScreenshotAction();

                case ActionTypes.BeginScrollingScreenshot: return new BeginScrollingScreenshotAction();
                case ActionTypes.ContinueScrollingScreenshot: return new ContinueScrollingScreenshotAction();
                case ActionTypes.EndScrollingScreenshot: return new EndScrollingScreenshotAction();

                case ActionTypes.ShowPreview: return new ShowPreviewAction();
                case ActionTypes.HidePreview: return new HidePreviewAction();

                case ActionTypes.Heart: return new HeartAction();
                case ActionTypes.Save: return new SaveAction();
                case ActionTypes.Upload: return new UploadAction();
                case ActionTypes.ApplyEdits: return new ApplyEditsAction();
                case ActionTypes.Delete: return new DeleteAction();

                case ActionTypes.Run: return new RunAction();
                case ActionTypes.Clipboard: return new ClipboardAction();
                default: return new NoneAction();
            }
        }
    }
}
