using DiscordRPC;
using DiscordRPC.Message;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Logging;
using Yuzaki.Game.Audio;
using Yuzaki.Game.OsuElement;

namespace Yuzaki.Desktop;

public partial class DiscordRichPresence : Component
{
    private const string client_id = "1137470190217592934";

    private DiscordRpcClient client = null!;

    [Resolved]
    private YuzakiPlayerManager playerManager { get; set; }

    [Resolved]
    private OsuStableDatabase osuStableDatabase { get; set; }

    private readonly RichPresence presence = new RichPresence
    {
        Assets = new Assets
        {
            LargeImageKey = "yuzaki_logo"
        }
    };

    [BackgroundDependencyLoader]
    private void load()
    {
        client = new DiscordRpcClient(client_id)
        {
            SkipIdenticalPresence = false
        };

        client.OnReady += onReady;

        client.OnError += (sender, args) => Logger.Log($"Discord RPC error: {args.Message}", LoggingTarget.Network);

        playerManager.CurrentBeatmap.BindValueChanged(_ => updateStatus());
        playerManager.Playing.BindValueChanged(_ => updateSmallIcon());

        client.Initialize();
        initialize();
    }

    private void onReady(object sender, ReadyMessage args)
    {
        Logger.Log("Discord RPC is ready!", LoggingTarget.Network);
        updateStatus();
    }

    private void initialize()
    {
        presence.Assets.LargeImageKey = "yuzaki_logo";
        presence.Assets.LargeImageText = "Yuzaki";
        presence.Assets.SmallImageKey = "stop";
        presence.Assets.SmallImageText = "Idle";
        presence.State = "Idle";
        client.SetPresence(presence);
    }

    private void updateStatus()
    {
        if (!client.IsInitialized)
            return;

        // Update text
        if (playerManager.CurrentBeatmap == null)
        {
            presence.State = "Idle";
            presence.Details = string.Empty;
            presence.Assets.LargeImageKey = "yuzaki_logo";
        }
        else
        {
            presence.Details = playerManager.CurrentBeatmap.Value.Artist;
            presence.State = playerManager.CurrentBeatmap.Value.Title;
            presence.Assets.LargeImageKey = $"https://assets.ppy.sh/beatmaps/{playerManager.CurrentBeatmap.Value.BeatmapSetId}/covers/list.jpg";
            presence.Assets.LargeImageText = $"{playerManager.CurrentBeatmap.Value.Artist} - {playerManager.CurrentBeatmap.Value.Title}";
        }

        client.SetPresence(presence);
    }

    private void updateSmallIcon()
    {
        // update small icon
        if (playerManager.CurrentBeatmap == null)
        {
            presence.Assets.SmallImageKey = "stop";
            presence.Assets.SmallImageText = "Idle";
        }
        else
        {
            presence.Assets.SmallImageKey = playerManager.IsPlaying() ? "play" : "pause";
            presence.Assets.SmallImageText = playerManager.IsPlaying() ? "Playing" : "Paused";
        }

        client.SetPresence(presence);
    }
}
