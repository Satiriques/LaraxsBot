using MalParser.Interfaces;
using Microsoft.VisualBasic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MalParserTests")]
namespace MalParser.Models
{
    internal class Anime : IAnime, IEquatable<Anime>
    {
        public string Background { get; set; } = string.Empty;

        public string[] Genres { get; set; } = new string[] { };

        public string[] Licensors { get; set; } = new string[] { };

        public int NumberOfEpisodes { get; set; }

        public int PopularityPosition { get; set; }

        public string[] Producers { get; set; } = new string[] { };

        public int RankedPosition { get; set; }

        public float Score { get; set; }

        public string Source { get; set; } = string.Empty;

        public string[] Studios { get; set; } = new string[] { };

        public string Synopsis { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public bool Equals([AllowNull]Anime other)
        {
            if (other == null)
            {
                return false;
            }

            return Background.Equals(other.Background)
                && (ReferenceEquals(Genres, other.Genres)
                    || Enumerable.SequenceEqual(Genres, other.Genres))
                && (ReferenceEquals(Licensors, other.Licensors)
                    || Enumerable.SequenceEqual(Licensors, other.Licensors))
                && NumberOfEpisodes.Equals(other.NumberOfEpisodes)
                && PopularityPosition.Equals(other.PopularityPosition)
                && (ReferenceEquals(Producers, other.Producers)
                    || Enumerable.SequenceEqual(Producers, other.Producers))
                && RankedPosition.Equals(other.RankedPosition)
                && Score.Equals(other.Score)
                && Source.Equals(other.Source)
                && (ReferenceEquals(Studios, other.Studios)
                    || Enumerable.SequenceEqual(Studios, other.Studios))
                && Synopsis.Equals(other.Synopsis)
                && Title.Equals(other.Title)
                && Type.Equals(other.Type)
                && ImageUrl.Equals(other.ImageUrl);
        }
    }
}
