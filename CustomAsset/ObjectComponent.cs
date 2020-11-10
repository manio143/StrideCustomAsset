using Stride.Engine;
using Stride.Core.Mathematics;

namespace CustomAsset
{
    /// <summary>
    /// Example component that takes a reference to an <see cref="Object"/>.
    /// </summary>
    public class ObjectComponent : SyncScript
    {
        public Object Object { get; set; }

        public override void Start()
        {
            // you can set Object from the GameStudio or load it like that
            if (Object == null)
                Object = Content.Load<Object>("MyObject");
        }

        public override void Update()
        {
            DebugText.Print($"Object value: {Object.Value}", new Int2(20));
        }
    }
}
