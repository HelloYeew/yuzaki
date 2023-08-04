using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.IO.Stores;
using osu.Framework.Logging;
using osu.Framework.Platform;
using osuTK;
using Yuzaki.Game.OsuElement;
using Yuzaki.Game.Store;

namespace Yuzaki.Game.Graphics.Components
{
    public partial class ProfilePictureMenu : CompositeDrawable
    {
        public static readonly int ICON_SIZE = 50;

        [Resolved]
        private OsuStableDatabase database { get; set; }

        [Resolved]
        private YuzakiTextureStore textureStore { get; set; }

        [Resolved]
        private GameHost host { get; set; }

        private Texture defaultProfilePictureTexture;
        private Sprite profilePictureSprite;
        private OnlineStore onlineStore = new OnlineStore();

        [BackgroundDependencyLoader]
        private void load()
        {
            defaultProfilePictureTexture = textureStore.Get("default_profile_picture.jpg");

            InternalChildren = new Drawable[]
            {
                new Circle
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(ICON_SIZE),
                    Colour = YuzakiColour.ProfilePictureBackgroundColour
                },
                new Container
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(ICON_SIZE),
                    Masking = true,
                    CornerRadius = 28,
                    Children = new Drawable[]
                    {
                        profilePictureSprite = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Size = new Vector2(1f)
                        }
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Thread getImageThread = new Thread(() =>
            {
                string playerName = database.OsuDatabase.AccountName;

                // try request to https://osu.ppy.sh/u/<playerName> and see what new url is it redirect to
                // It should be redirect to https://osu.ppy.sh/users/<playerName>
                // Then redirect to https://osu.ppy.sh/users/<playerId> and get the profile ID from the url
                // Then we can get the profile picture from https://a.ppy.sh/<playerId>

                HttpClientHandler httpClientHandler = new HttpClientHandler()
                {
                    AllowAutoRedirect = false
                };
                HttpClient httpClient = new HttpClient(httpClientHandler);
                var response = httpClient.GetAsync($"https://osu.ppy.sh/u/{playerName}").Result;

                Logger.Log($"Request to https://osu.ppy.sh/u/{playerName} success with status code {response.StatusCode}");
                var targetUrl = response.StatusCode == HttpStatusCode.Redirect
                    ? response.Headers.Location?.OriginalString
                    : null;

                if (targetUrl == null)
                {
                    Logger.Log("Failed to get profile ID from redirect method, using default profile picture instead.");
                    profilePictureSprite.Texture = defaultProfilePictureTexture;
                    return;
                }
                else
                {
                    Logger.Log($"Redirected to {targetUrl}");
                }

                response = httpClient.GetAsync(targetUrl).Result;
                targetUrl = response.StatusCode == HttpStatusCode.Redirect
                    ? response.Headers.Location?.OriginalString
                    : null;

                if (targetUrl == null)
                {
                    Logger.Log("Failed to get profile ID from second redirect method, using default profile picture instead.");
                    profilePictureSprite.Texture = defaultProfilePictureTexture;
                    return;
                }
                else
                {
                    Logger.Log($"Redirected to {targetUrl}");
                }

                string profileId = targetUrl.Split('/')[4];

                Logger.Log($"Got profile ID {profileId} from redirect method, requesting profile picture from https://a.ppy.sh/{profileId}");

                Stream stream = onlineStore.GetStream($"https://a.ppy.sh/{profileId}");
                profilePictureSprite.Texture = Texture.FromStream(host.Renderer, stream);
            });
            getImageThread.Start();
        }
    }
}
