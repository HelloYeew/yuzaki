﻿using System;
using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osuTK;
using Yuzaki.Game.Audio;
using Yuzaki.Game.OsuElement;
using Yuzaki.Game.Store;
using Yuzaki.Game.Utils;

namespace Yuzaki.Game.Graphics.Components
{
    public partial class MusicPlayer : CompositeDrawable
    {
        [Resolved]
        private YuzakiPlayerManager playerManager { get; set; }

        [Resolved]
        private YuzakiTextureStore textureStore { get; set; }

        [Resolved]
        private GameHost host { get; set; }

        private YuzakiSpriteText songNameText;
        private YuzakiSpriteText artistNameText;
        private Sprite coverArtSprite;
        private Texture defaultCoverArtTexture;
        private YuzakiSpriteText currentTimeText;
        private YuzakiSpriteText totalTimeText;
        private ProgressBar progressBar;
        private CircleIconButton playButton;
        private IconButton previousButton;
        private IconButton nextButton;

        [BackgroundDependencyLoader]
        private void load()
        {
            InternalChildren = new Drawable[]
            {
                new Container
                {
                    Anchor = Anchor.BottomCentre,
                    Origin = Anchor.BottomCentre,
                    RelativeSizeAxes = Axes.Both,
                    Width = 0.98f,
                    Height = 0.12f,
                    Masking = true,
                    CornerRadius = 10,
                    Margin = new MarginPadding(YuzakiStylingEnum.SCREEN_PADDING),
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = YuzakiColour.MusicPlayerBackgroundColour,
                            Alpha = 0.8f
                        },
                        new FillFlowContainer()
                        {
                            RelativeSizeAxes = Axes.Both,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Direction = FillDirection.Horizontal,
                            Spacing = new Vector2(10),
                            Children = new Drawable[]
                            {
                                new Container()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Size = new Vector2(100),
                                    Child = new Container()
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        Size = new(80),
                                        Masking = true,
                                        CornerRadius = YuzakiStylingEnum.CORNER_RADIUS,
                                        Child = coverArtSprite = new Sprite()
                                        {
                                            Anchor = Anchor.Centre,
                                            Origin = Anchor.Centre,
                                            RelativeSizeAxes = Axes.Both,
                                            Texture = defaultCoverArtTexture = textureStore.Get("default_album.jpg"),
                                            FillMode = FillMode.Fill
                                        }
                                    }
                                },
                                new Container()
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(0.2f, 1),
                                    Child = new FillFlowContainer()
                                    {
                                        Anchor = Anchor.CentreLeft,
                                        Origin = Anchor.CentreLeft,
                                        RelativeSizeAxes = Axes.Both,
                                        Direction = FillDirection.Vertical,
                                        Spacing = new Vector2(5),
                                        Children = new Drawable[]
                                        {
                                            songNameText = new YuzakiSpriteText()
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Text = "No beatmap selected",
                                                Font = YuzakiFont.GetFont(size: 30f, weight: YuzakiFont.FontWeight.Bold),
                                            },
                                            artistNameText = new YuzakiSpriteText()
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Text = "Please select a beatmap to play!",
                                                Font = YuzakiFont.GetFont(size: 20f)
                                            }
                                        }
                                    }
                                },
                                new Container
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    RelativeSizeAxes = Axes.Both,
                                    Size = new Vector2(0.35f, 0.6f),
                                    Child = new FillFlowContainer()
                                    {
                                        Anchor = Anchor.Centre,
                                        Origin = Anchor.Centre,
                                        RelativeSizeAxes = Axes.Both,
                                        Height = 0.7f,
                                        Direction = FillDirection.Vertical,
                                        Children = new Drawable[]
                                        {
                                            // Controls
                                            new FillFlowContainer()
                                            {
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                                RelativeSizeAxes = Axes.Both,
                                                Direction = FillDirection.Horizontal,
                                                Spacing = new Vector2(30),
                                                Children = new Drawable[]
                                                {
                                                    previousButton = new IconButton(FontAwesome.Solid.StepBackward)
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Size = new Vector2(35)
                                                    },
                                                    playButton = new CircleIconButton(FontAwesome.Solid.Play)
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Size = new Vector2(40)
                                                    },
                                                    nextButton = new IconButton(FontAwesome.Solid.StepForward)
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Size = new Vector2(35)
                                                    },
                                                },
                                            },
                                            // Progress bar
                                            new FillFlowContainer()
                                            {
                                                Anchor = Anchor.Centre,
                                                Origin = Anchor.Centre,
                                                RelativeSizeAxes = Axes.Both,
                                                Direction = FillDirection.Horizontal,
                                                Spacing = new Vector2(5),
                                                Children = new Drawable[]
                                                {
                                                    currentTimeText = new YuzakiSpriteText()
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Text = "0:00",
                                                        Colour = YuzakiColour.MusicPlayerTimeTextColour,
                                                        Font = YuzakiFont.GetFont(size: 16f)
                                                    },
                                                    new Container()
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        RelativeSizeAxes = Axes.X,
                                                        Height = 10,
                                                        Masking = true,
                                                        CornerRadius = 5,
                                                        Child = progressBar = new ProgressBar(true)
                                                        {
                                                            Anchor = Anchor.CentreLeft,
                                                            Origin = Anchor.CentreLeft,
                                                            RelativeSizeAxes = Axes.X,
                                                            Height = 10,
                                                            FillColour = YuzakiColour.MusicPlayerProgressBarForegroundColour,
                                                            BackgroundColour = YuzakiColour.MusicPlayerProgressBarBackgroundColour,
                                                            OnSeek = (progress) =>
                                                            {
                                                                if (playerManager.CurrentBeatmap != null)
                                                                {
                                                                    playerManager.Seek(progress);
                                                                }
                                                            }
                                                        }
                                                    },
                                                    totalTimeText = new YuzakiSpriteText()
                                                    {
                                                        Anchor = Anchor.Centre,
                                                        Origin = Anchor.Centre,
                                                        Text = "0:00",
                                                        Colour = YuzakiColour.MusicPlayerTimeTextColour,
                                                        Font = YuzakiFont.GetFont(size: 16f)
                                                    }
                                                }
                                            }
                                        }
                                    }
                                },
                                // Volume slider
                                // TODO: Set alpha back when volume slider is implemented
                                new Container
                                {
                                    Anchor = Anchor.CentreLeft,
                                    Origin = Anchor.CentreLeft,
                                    Size = new Vector2(100),
                                    Alpha = 0,
                                    Child = new FillFlowContainer()
                                    {
                                        Anchor = Anchor.CentreLeft,
                                        Origin = Anchor.CentreLeft,
                                        RelativeSizeAxes = Axes.Both,
                                        Direction = FillDirection.Horizontal,
                                        Spacing = new Vector2(5),
                                        Children = new Drawable[]
                                        {
                                            new IconButton(FontAwesome.Solid.VolumeUp)
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                Size = new Vector2(50)
                                            },
                                            new Container()
                                            {
                                                Anchor = Anchor.CentreLeft,
                                                Origin = Anchor.CentreLeft,
                                                RelativeSizeAxes = Axes.X,
                                                Height = 10,
                                                Masking = true,
                                                CornerRadius = 5,
                                                Children = new Drawable[]
                                                {
                                                    new Box()
                                                    {
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        RelativeSizeAxes = Axes.X,
                                                        Height = 10,
                                                        Colour = YuzakiColour.MusicPlayerVolumeSliderBackgroundColour
                                                    },
                                                    new Box()
                                                    {
                                                        Anchor = Anchor.CentreLeft,
                                                        Origin = Anchor.CentreLeft,
                                                        RelativeSizeAxes = Axes.X,
                                                        Width = 0.5f,
                                                        Height = 10,
                                                        Colour = YuzakiColour.MusicPlayerVolumeSliderForegroundColour
                                                    },
                                                }
                                            }
                                        },
                                    }
                                }
                            }
                        }
                    }
                }
            };

            playerManager.CurrentBeatmap.BindValueChanged(beatmap =>
            {
                try
                {
                    if (beatmap.NewValue == null)
                    {
                        Logger.Log("No beatmap selected or beatmap is null, using default cover art instead.");
                        songNameText.Text = "No beatmap selected";
                        artistNameText.Text = "Please select a beatmap to play!";
                        coverArtSprite.Texture = defaultCoverArtTexture;
                    }

                    // Update text
                    songNameText.Text = beatmap.NewValue?.Title;
                    artistNameText.Text = beatmap.NewValue?.Artist;

                    // Update cover art
                    string backgroundPath = Utility.GetBeatmapBackgroundPath(beatmap.NewValue);

                    if (backgroundPath == null)
                    {
                        Logger.Log($"Beatmap {beatmap.NewValue?.Artist} - {beatmap.NewValue?.Title} ({beatmap.NewValue?.BeatmapId}) doesn't have a background, using default cover art instead.");
                        coverArtSprite.Texture = defaultCoverArtTexture;
                    }
                    else
                    {
                        Logger.Log($"Found art for beatmap {beatmap.NewValue?.Artist} - {beatmap.NewValue?.Title} ({beatmap.NewValue?.BeatmapId}) at {backgroundPath}.");
                        FileStream fileStream = new FileStream(backgroundPath, FileMode.Open, FileAccess.Read);
                        coverArtSprite.Texture = Texture.FromStream(host.Renderer, fileStream);
                        fileStream.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Logger.Error(e, "Failed to load image");
                    Logger.Log("Using default background instead.");
                    coverArtSprite.Texture = defaultCoverArtTexture;
                }

                updateTotalTime();
            });

            playButton.Action = () =>
            {
                if (playerManager.CurrentBeatmap.Value == null)
                {
                    Logger.Log("No beatmap selected, cannot play.");
                    return;
                }

                if (playerManager.IsPlaying())
                {
                    playerManager.Pause();
                }
                else
                {
                    playerManager.Play();
                }
            };

            previousButton.Action = () =>
            {
                playerManager.Previous();
            };

            nextButton.Action = () =>
            {
                playerManager.Next();
            };

            playerManager.Playing.BindValueChanged(playing =>
            {
                playButton.IconSprite.Icon = playing.NewValue ? FontAwesome.Solid.Pause : FontAwesome.Solid.Play;
            });
        }

        protected override void Update()
        {
            base.Update();

            updateCurrentTime();
            progressBar.EndTime = playerManager.CurrentBeatmap.Value == null ? 0 : playerManager.GetTotalTime();
            progressBar.CurrentTime = playerManager.CurrentBeatmap.Value == null ? 0 : playerManager.GetCurrentTime();

            // Allow auto skip if already playing
            if (playerManager.CurrentBeatmap.Value != null)
            {
                if (playerManager.HasEnded())
                {
                    playerManager.Next();
                }
            }
        }

        private void updateCurrentTime()
        {
            currentTimeText.Text = playerManager.CurrentBeatmap.Value == null ? "0:00" : TextFormatUtils.GetFormattedTime(playerManager.GetCurrentTime());
        }

        private void updateTotalTime()
        {
            totalTimeText.Text = playerManager.CurrentBeatmap.Value == null ? "0:00" : TextFormatUtils.GetFormattedTime(playerManager.GetTotalTime());
        }

        private void updateTrackText()
        {
            songNameText.Text = playerManager.CurrentBeatmap.Value?.Title;
        }
    }
}
