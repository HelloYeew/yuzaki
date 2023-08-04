using System;
using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Platform;
using Yuzaki.Game.Audio;
using Yuzaki.Game.OsuElement;
using Yuzaki.Game.Store;

namespace Yuzaki.Game.Graphics
{
    public partial class YuzakiBackground : CompositeDrawable
    {
        [Resolved]
        private YuzakiPlayerManager playerManager { get; set; }

        [Resolved]
        private YuzakiTextureStore textureStore { get; set; }

        [Resolved]
        private GameHost host { get; set; }

        private Sprite backgroundSprite;

        private Texture defaultBackgroundTexture;

        [BackgroundDependencyLoader]
        private void load()
        {
            defaultBackgroundTexture = textureStore.Get("default_background.jpg");
            InternalChildren = new Drawable[]
            {
                backgroundSprite = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    FillMode = FillMode.Fill,
                    Texture = defaultBackgroundTexture
                },
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Colour = YuzakiColour.BackgroundColour,
                    Alpha = 0.8f
                }
            };

            playerManager.CurrentBeatmap.BindValueChanged(beatmap =>
            {
                try
                {
                    if (beatmap.NewValue == null)
                    {
                        Logger.Log("No beatmap selected or beatmap is null, using default background instead.");
                        backgroundSprite.Texture = defaultBackgroundTexture;
                        return;
                    }

                    string backgroundPath = Utility.GetBeatmapBackgroundPath(beatmap.NewValue);

                    if (backgroundPath == null)
                    {
                        Logger.Log($"Beatmap {beatmap.NewValue.Artist} - {beatmap.NewValue.Title} ({beatmap.NewValue.BeatmapId}) doesn't have a background, using default background instead.");
                        backgroundSprite.Texture = defaultBackgroundTexture;
                    }
                    else
                    {
                        Logger.Log($"Found background for beatmap {beatmap.NewValue.Artist} - {beatmap.NewValue.Title} ({beatmap.NewValue.BeatmapId}) at {backgroundPath}.");
                        FileStream fileStream = new FileStream(backgroundPath, FileMode.Open, FileAccess.Read);
                        backgroundSprite.Texture = Texture.FromStream(host.Renderer, fileStream);
                        fileStream.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Failed to load background.");
                    Logger.Log("Using default background instead.");
                    backgroundSprite.Texture = defaultBackgroundTexture;
                }
            }, true);
        }
    }
}
