using Stride.Assets;
using Stride.Core;
using Stride.Core.Assets;
using Stride.Core.Assets.Compiler;
using Stride.Core.BuildEngine;
using Stride.Core.Serialization;
using Stride.Core.Serialization.Contents;
using System.Threading.Tasks;

namespace CustomAsset
{
    // This file introduces a new type of asset.
    // There's a runtime object used during the game.
    // There's an asset object used during design (this is the contents of an asset file).
    // There's a compiler, invoked by AssetCompilerApp which transforms
    //   design time asset into runtime asset.

    /// <summary>
    /// Runtime object for the custom asset.
    /// </summary>
    [DataContract("Object")]
    [ContentSerializer(typeof(DataContentSerializerWithReuse<Object>))]
    [ReferenceSerializer, DataSerializerGlobal(typeof(ReferenceSerializer<Object>), Profile = "Content")]
    public sealed class Object
    {
        // put your custom properties here that will be available during game runtime
        public int Value { get; set; }
    }

    /// <summary>
    /// Design time object for the custom asset.
    /// </summary>
    [DataContract("ObjectAsset")]
    [AssetDescription(FileExtension, AllowArchetype = false)]
    [AssetContentType(typeof(Object))]
    [AssetFormatVersion(StrideConfig.PackageName, CurrentVersion, "2.0.0.0")]
    public sealed class ObjectAsset : Asset
    {
        private const string CurrentVersion = "2.0.0.0";
        public const string FileExtension = ".sdobj";

        // description of an asset which after compilation will result in the runtime object
        public string Value { get; set; }
    }

    /// <summary>
    /// Compiler for the custom asset.
    /// </summary>
    [AssetCompiler(typeof(ObjectAsset), typeof(AssetCompilationContext))]
    public sealed class ObjectAssetCompiler : AssetCompilerBase
    {
        protected override void Prepare(AssetCompilerContext context, AssetItem assetItem, string targetUrlInStorage, AssetCompilerResult result)
        {
            var asset = (ObjectAsset)assetItem.Asset;

            // you can have many build steps, each one is running an AssetCommand
            result.BuildSteps = new AssetBuildStep(assetItem);
            result.BuildSteps.Add(new ObjectAssetCommand(targetUrlInStorage, asset, assetItem.Package));
        }

        /// <summary>
        /// An <see cref="AssetCommand"/> that converts design time asset into runtime asset.
        /// </summary>
        public class ObjectAssetCommand : AssetCommand<ObjectAsset>
        {
            public ObjectAssetCommand(string url, ObjectAsset parameters, IAssetFinder assetFinder) : base(url, parameters, assetFinder)
            {
            }

            protected override Task<ResultStatus> DoCommandOverride(ICommandContext commandContext)
            {
                var assetManager = new ContentManager(MicrothreadLocalDatabases.ProviderService);

                // you can do your custom processing here
                var @object = new Object
                {
                    Value = int.TryParse(Parameters.Value, out var num) ? num : 0,
                };
                assetManager.Save(Url, @object);

                commandContext.Logger.Info($"Saving custom asset: Object {{ Value = {@object.Value} }}");

                return Task.FromResult(ResultStatus.Successful);
            }
        }
    }
}
