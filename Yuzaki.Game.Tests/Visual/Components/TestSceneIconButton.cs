using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Yuzaki.Game.Graphics;
using Yuzaki.Game.Graphics.Components;

namespace Yuzaki.Game.Tests.Visual.Components;

[TestFixture]
public partial class TestSceneIconButton : YuzakiTestScene
{
    public TestSceneIconButton()
    {
        Add(new YuzakiScreenStack());
        Add(new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Child = new IconButton(FontAwesome.Solid.Play)
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(100)
            }
        });
    }
}
