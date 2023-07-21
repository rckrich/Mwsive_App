using System;
using System.Collections.Generic;

#region Mwsive Entities

public class MwsiveUser
{
    public string id { get; set; }
    public string email { get; set; }
    public string genre { get; set; }
    public int age { get; set; }
}

public class TrackAction
{
    public int user_id { get; set; }
    public int track_id { get; set; }
    public string action { get; set; }
    public float duration { get; set; }
}

public class MusicalDNA
{
    public string type { get; set; }
    public List<string> track_ids { get; set; }
}

public class Badge
{
    public int id;
}

public class Settings
{
    public int id;
}

public class Challenges
{
    public int id;
}

public class Advertising
{
    public int id;
}

public class RecommendedArtist
{
    public int id;
}

public class RecommendedPlaylist
{
    public int id;
}

public class RecommendedTrack
{
    public int id;
}

public class RecommendedAlbum
{
    public int id;
}

public class Genre
{
    public int id;
}

#endregion

#region Json Convert Classes

public class MwsiveUserRoot
{
    public string MwsiveUser { get; set; }
}

public class MwsiveLoginRoot
{
    public MwsiveUser MwsiveUser { get; set; }
    public string mwsive_token { get; set; }
}

public class MwsiveCuratorsRoot
{
    public List<MwsiveUser> curators { get; set; } 
}

public class MwsiveFollowersRoot
{
    public List<MwsiveUser> followers { get; set; }
}

public class MwsiveBadgesRoot
{
    public List<Badge> badges { get; set; }
}

public class MwsiveRankingRoot
{
    public List<MwsiveUser> users { get; set; }
}

public class MwsiveSettingsRoot
{
    public List<Settings> settings { get; set; }
}

public class MwsiveChallengesRoot
{
    public List<Challenges> challenges { get; set; }
}

public class MwsiveCompleteChallengesRoot
{
    public int challenge_id { get; set; }
    public int points_to_add { get; set; }
}

public class MwsiveAdvertisingRoot
{
    public List<Advertising> advertising { get; set; }
}

public class MwsiveRecommendedCuratorsRoot
{
    public List<MwsiveUser> users { get; set; }
}

public class MwsiveRecommendedArtistsRoot
{
    public List<RecommendedArtist> recommended_artists { get; set; }
}

public class MwsiveRecommendedPlaylistRoot
{
    public List<RecommendedPlaylist> recommended_playlists { get; set; }
}

public class MwsiveRecommendedTrackRoot
{
    public List<RecommendedTrack> recommended_tracks { get; set; }
}

public class MwsiveRecommendedAlbumRoot
{
    public List<RecommendedAlbum> recommended_albums { get; set; }
}

public class MwsiveGenresRoot
{
    public List<Genre> genres { get; set; }
}

#endregion