using DeezNET.Data;
using Shouldly;

namespace DeezNET.Tests
{
    public class DeezerURLTests
    {
        [Theory]
        [InlineData("https://www.deezer.com/en/track/13787884", 13787884, EntityType.Track)]
        [InlineData("https://www.deezer.com/en/track/425192882/", 425192882, EntityType.Track)]
        [InlineData("https://www.deezer.com/en/playlist/1477723445", 1477723445, EntityType.Playlist)]
        [InlineData("https://www.deezer.com/en/playlist/2296059482/", 2296059482, EntityType.Playlist)]
        [InlineData("https://www.deezer.com/en/album/179885792", 179885792, EntityType.Album)]
        [InlineData("https://www.deezer.com/en/album/2795241/", 2795241, EntityType.Album)]
        [InlineData("https://www.deezer.com/en/artist/1604/top_track", 1604, EntityType.ArtistTop)]
        [InlineData("https://www.deezer.com/en/artist/12983465/top_track/", 12983465, EntityType.ArtistTop)]
        [InlineData("https://www.deezer.com/en/artist/566920", 566920, EntityType.Artist)]
        [InlineData("https://www.deezer.com/en/artist/181120627/", 181120627, EntityType.Artist)]
        public void ParseFullUrlTest(string url, long id, EntityType type)
        {
            var data = DeezerURL.Parse(url);
            data.ShouldNotBeNull();
            data.Id.ShouldBe(id);
            data.EntityType.ShouldBe(type);
        }

        [Theory]
        [InlineData("https://www.deezer.com/en/track/13787884", true, 13787884, EntityType.Track)]
        [InlineData("https://www.deezer.com/en/this_is_not_an_entity_type/425192882", false)]
        [InlineData("https://www.deezer.com/en/playlist/1477723445", true, 1477723445, EntityType.Playlist)]
        [InlineData("https://www.deezer.com/en/playlist/this_is_not_a_number", false)]
        public void TryParseFullUrlTest(string url, bool parsable, long id = 0, EntityType type = EntityType.Track)
        {
            bool parsed = DeezerURL.TryParse(url, out DeezerURL data);
            parsed.ShouldBeEquivalentTo(parsable);

            if (parsable)
            {
                data.ShouldNotBeNull();
                data.Id.ShouldBe(id);
                data.EntityType.ShouldBe(type);
            }
            else
                data.ShouldBeNull();
        }
    }
}