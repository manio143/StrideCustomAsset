using Stride.Core.Annotations;
using Stride.Core.Assets.Editor.ViewModel;
using Stride.Core.Diagnostics;
using Stride.Editor;

namespace CustomAsset
{
    public class CustomEditorPlugin : StrideAssetsPlugin
    {
        public override void InitializeSession([NotNull] SessionViewModel session)
        {
        }

        protected override void Initialize(ILogger logger)
        {
            logger.Warning("Initializing custom plugin.");
        }
    }
}
