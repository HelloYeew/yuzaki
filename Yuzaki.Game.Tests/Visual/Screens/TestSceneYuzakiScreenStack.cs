using NUnit.Framework;
using Yuzaki.Game.Graphics;

namespace Yuzaki.Game.Tests.Visual.Screens;

[TestFixture]
public partial class TestSceneYuzakiScreenStack : YuzakiTestScene
{
    public TestSceneYuzakiScreenStack()
    {
        Add(new YuzakiScreenStack());
    }
}
