namespace Yuzaki.DatabaseReader.Stable.OsuElement.Components.Events
{
    public class SpriteEvent : EventBase
    {
        internal SpriteEvent()
        {
        }

        public string Layer { get; internal set; }
        public string Origin { get; internal set; }
        public string Path { get; internal set; }
        public float X { get; internal set; }
        public float Y { get; internal set; }
    }
}
