using Stride.Engine;

namespace CustomAsset
{
    class CustomAssetApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
