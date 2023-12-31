﻿using osu.Framework.Graphics;
using osu.Framework.Screens;
using Yuzaki.Game.Graphics.Components;

namespace Yuzaki.Game.Graphics.Screens;

public partial class MainPlayerScreenStack : ScreenStack
{
    public MusicPlayer MusicPlayerComponent;

    public MainPlayerScreenStack()
    {
        InternalChildren = new Drawable[]
        {
            MusicPlayerComponent = new MusicPlayer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                RelativeSizeAxes = Axes.Both
            }
        };
    }
}
