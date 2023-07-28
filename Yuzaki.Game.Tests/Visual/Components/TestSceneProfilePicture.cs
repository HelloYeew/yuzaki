using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;
using Yuzaki.Game.Graphics;
using Yuzaki.Game.Graphics.Components;

namespace Yuzaki.Game.Tests.Visual.Components;

[TestFixture]
public partial class TestSceneProfilePicture : YuzakiTestScene
{
    public TestSceneProfilePicture()
    {
        Add(new YuzakiBackground
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
        });
        Add(new Container
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            RelativeSizeAxes = Axes.Both,
            Child = new ProfilePictureMenu
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Size = new Vector2(100)
            }
        });
    }
}
