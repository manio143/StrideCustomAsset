using Stride.Core;
using Stride.Core.Assets.Editor.Services;
using Stride.Core.Reflection;

namespace CustomAsset
{
    internal class Module
    {
        [ModuleInitializer]
        public static void Initialize()
        {
            AssemblyRegistry.Register(typeof(Module).Assembly, AssemblyCommonCategories.Assets);

            // Experimenting to see if we can load custom extensions
            AssetsPlugin.RegisterPlugin(typeof(CustomEditorPlugin));
        }
    }
}
