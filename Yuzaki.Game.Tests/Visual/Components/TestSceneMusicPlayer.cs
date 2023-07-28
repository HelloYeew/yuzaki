using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using Yuzaki.Game.Graphics;
using Yuzaki.Game.Graphics.Components;

namespace Yuzaki.Game.Tests.Visual.Components;

[TestFixture]
public partial class TestSceneMusicPlayer : YuzakiTestScene
{
    public TestSceneMusicPlayer()
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
            Size = new(1f),
            Child = new MusicPlayer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            }
        });
    }
}
