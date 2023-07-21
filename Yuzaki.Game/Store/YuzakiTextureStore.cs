using osu.Framework.Graphics.Rendering;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;

namespace Yuzaki.Game.Store;

public class YuzakiTextureStore : LargeTextureStore
{
    public YuzakiTextureStore(IRenderer renderer, IResourceStore<TextureUpload> store)
        : base(renderer, store)
    {
    }
}
