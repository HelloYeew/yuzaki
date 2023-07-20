using System;
using osu.Framework.Graphics.Sprites;

namespace Yuzaki.Game.Graphics
{
    public class YuzakiFont
    {
        /// <summary>
        /// The default font size.
        /// </summary>
        public const float DEFAULT_SIZE = 16;

        /// <summary>
        /// The default font.
        /// </summary>
        public static FontUsage Default => GetFont();

        public static FontUsage Saira => GetFont(Typeface.Saira, weight: FontWeight.Regular);
        public static FontUsage SairaCondensed => GetFont(Typeface.SairaCondensed, weight: FontWeight.Regular);
        public static FontUsage NotoSansJapanese => GetFont(Typeface.NotoSansJP, weight: FontWeight.Regular);
        public static FontUsage NotoSerifJapanese => GetFont(Typeface.NotoSerifJP, weight: FontWeight.Regular);
        public static FontUsage NotoSansThai => GetFont(Typeface.NotoSansTH, weight: FontWeight.Regular);
        public static FontUsage Comfortaa => GetFont(Typeface.Comfortaa, weight : FontWeight.Regular);

        /// <summary>
        /// Retrieve a <see cref="FontUsage"/> with some more specified requirements.
        /// </summary>
        /// <param name="typeface">The font typeface.</param>
        /// <param name="size">The size of the font.</param>
        /// <param name="weight">The font weight.</param>
        /// <param name="italics">Whether the font is italic.</param>
        /// <param name="fixedWidth">Whether all characters should be spaced the same distance apart.</param>
        /// <returns>The <see cref="FontUsage"/></returns>
        public static FontUsage GetFont(Typeface typeface = Typeface.Saira, float size = DEFAULT_SIZE,
            FontWeight weight = FontWeight.Regular, bool italics = false, bool fixedWidth = false)
            => new FontUsage(GetFamilyString(typeface), size, GetWeightString(typeface, weight), getItalics(typeface, italics),
                fixedWidth);

        /// <summary>
        /// Return the availability of italics for the specified typeface.
        ///
        /// Will return false if the typeface is not supported, else will return as specified.
        /// </summary>
        /// <param name="typeface">The target typeface of the font.</param>
        /// <param name="italics">Whether want italic or not.</param>
        /// <returns>The boolean value that can use in <see cref="FontUsage"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static bool getItalics(Typeface typeface, bool italics)
        {
            // This can add some exception when some of our font are not support italics
            switch (typeface)
            {
                case Typeface.Saira:
                    return italics;

                case Typeface.SairaCondensed:
                    return italics;

                case Typeface.NotoSansJP:
                    return italics;

                case Typeface.NotoSerifJP:
                    return italics;

                case Typeface.NotoSansTH:
                    return italics;

                case Typeface.Comfortaa:
                    return italics;

                default:
                    throw new ArgumentOutOfRangeException(nameof(typeface), typeface, null);
            }
        }

        /// <summary>
        /// Retrieve the string representation of a <see cref="Typeface"/>
        /// </summary>
        /// <param name="typeface">The <see cref="Typeface"/></param>
        /// <returns>A string representation.</returns>
        public static string GetFamilyString(Typeface typeface)
        {
            switch (typeface)
            {
                case Typeface.Saira:
                    return "Saira";

                case Typeface.SairaCondensed:
                    return "Saira Condensed";

                case Typeface.NotoSansJP:
                    return "Noto Sans JP";

                case Typeface.NotoSerifJP:
                    return "Noto Serif JP";

                case Typeface.NotoSansTH:
                    return "Noto Sans Thai";

                case Typeface.Comfortaa:
                    return "Comfortaa";
            }

            return null;
        }

        /// <summary>
        /// Retrieve the string representation of a <see cref="FontWeight"/>
        /// </summary>
        /// <param name="typeface">The <see cref="Typeface"/>.</param>
        /// <param name="weight">The <see cref="FontWeight"/>.</param>
        /// <returns>The string representation of <paramref name="weight"/> in the specified <paramref name="typeface"/>.</returns>
        public static string GetWeightString(Typeface typeface, FontWeight weight)
        {
            return GetWeightString(GetFamilyString(typeface), weight);
        }

        /// <summary>
        /// Retrieve the string representation of a <see cref="FontWeight"/>.
        /// </summary>
        /// <param name="family">The family string.</param>
        /// <param name="weight">The <see cref="FontWeight"/>.</param>
        /// <returns>The string representation of <paramref name="weight"/> in the specified <paramref name="family"/>.</returns>
        public static string GetWeightString(string family, FontWeight weight) => weight.ToString();

        public enum Typeface
        {
            Saira,
            SairaCondensed,
            NotoSansJP,
            NotoSerifJP,
            NotoSansTH,
            Comfortaa
        }

        public enum FontWeight
        {
            Light = 300,
            Regular = 400,
            Medium = 500,
            SemiBold = 600,
            Bold = 700,
            Black = 900
        }
    }
}
