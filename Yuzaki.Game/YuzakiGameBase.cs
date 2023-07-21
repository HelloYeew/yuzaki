using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.IO.Stores;
using osuTK;
using Yuzaki.Game.Store;
using Yuzaki.Resources;

namespace Yuzaki.Game
{
    public partial class YuzakiGameBase : osu.Framework.Game
    {
        private YuzakiTextureStore textureStore;

        private DependencyContainer dependencies;

        protected override Container<Drawable> Content { get; }

        protected YuzakiGameBase()
        {
            // Ensure game and tests scale with window size and screen DPI.
            base.Content.Add(Content = new DrawSizePreservingFillContainer
            {
                // You may want to change TargetDrawSize to your "default" resolution, which will decide how things scale and position when using absolute coordinates.
                TargetDrawSize = new Vector2(1366, 768)
            });
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Resources.AddStore(new DllResourceStore(typeof(YuzakiResources).Assembly));

            AddFont(Resources, @"Fonts/Saira/Saira");
            AddFont(Resources, @"Fonts/Saira/Saira-Bold");
            AddFont(Resources, @"Fonts/Saira/Saira-BoldItalic");
            AddFont(Resources, @"Fonts/Saira/Saira-Italic");
            AddFont(Resources, @"Fonts/Saira/Saira-Light");
            AddFont(Resources, @"Fonts/Saira/Saira-LightItalic");

            AddFont(Resources, @"Fonts/SairaCondensed/SairaCondensed");
            AddFont(Resources, @"Fonts/SairaCondensed/SairaCondensed-Bold");
            AddFont(Resources, @"Fonts/SairaCondensed/SairaCondensed-BoldItalic");
            AddFont(Resources, @"Fonts/SairaCondensed/SairaCondensed-Italic");
            AddFont(Resources, @"Fonts/SairaCondensed/SairaCondensed-Light");
            AddFont(Resources, @"Fonts/SairaCondensed/SairaCondensed-LightItalic");

            AddFont(Resources, @"Fonts/NotoSansJP/NotoSansJP");
            AddFont(Resources, @"Fonts/NotoSansJP/NotoSansJP-Bold");
            AddFont(Resources, @"Fonts/NotoSansJP/NotoSansJP-BoldItalic");
            AddFont(Resources, @"Fonts/NotoSansJP/NotoSansJP-Italic");

            AddFont(Resources, @"Fonts/NotoSerifJP/NotoSerifJP");
            AddFont(Resources, @"Fonts/NotoSerifJP/NotoSerifJP-Bold");
            AddFont(Resources, @"Fonts/NotoSerifJP/NotoSerifJP-BoldItalic");
            AddFont(Resources, @"Fonts/NotoSerifJP/NotoSerifJP-Italic");

            AddFont(Resources, @"Fonts/NotoSansTH/NotoSansTH");
            AddFont(Resources, @"Fonts/NotoSansTH/NotoSansTH-Bold");
            AddFont(Resources, @"Fonts/NotoSansTH/NotoSansTH-BoldItalic");
            AddFont(Resources, @"Fonts/NotoSansTH/NotoSansTH-Italic");

            AddFont(Resources, @"Fonts/Comfortaa/Comfortaa-Regular");
            AddFont(Resources, @"Fonts/Comfortaa/Comfortaa-Bold");
            AddFont(Resources, @"Fonts/Comfortaa/Comfortaa-BoldItalic");
            AddFont(Resources, @"Fonts/Comfortaa/Comfortaa-Italic");
            AddFont(Resources, @"Fonts/Comfortaa/Comfortaa-Light");

            // Fallback fonts
            AddFont(Resources, @"Fonts/Noto/Noto-Basic");
            AddFont(Resources, @"Fonts/Noto/Noto-Hangul");
            AddFont(Resources, @"Fonts/Noto/Noto-CJK-Basic");
            AddFont(Resources, @"Fonts/Noto/Noto-CJK-Compatibility");

            dependencies.Cache(textureStore = new YuzakiTextureStore(Host.Renderer, Host.CreateTextureLoaderStore(new NamespacedResourceStore<byte[]>(Resources, "Textures"))));
            dependencies.CacheAs(this);
        }

        protected override IReadOnlyDependencyContainer CreateChildDependencies(IReadOnlyDependencyContainer parent) => dependencies = new DependencyContainer(base.CreateChildDependencies(parent));
    }
}
