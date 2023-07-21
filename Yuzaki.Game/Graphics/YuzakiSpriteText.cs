using osu.Framework.Graphics.Sprites;

namespace Yuzaki.Game.Graphics;

/// <summary>
/// The <see cref="SpriteText"/> with a default font used in the game.
/// </summary>
public partial class YuzakiSpriteText : SpriteText
{
    public YuzakiSpriteText()
    {
        Font = YuzakiFont.Default;
    }
}
