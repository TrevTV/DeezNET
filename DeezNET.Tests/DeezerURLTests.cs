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


        // i don't know if these short urls expire
        [Theory]
        [InlineData("https://deezer.page.link/PjpK8Ywdavbpn9FA8", "https://www.deezer.com/track/29373181?host=3103984544&utm_campaign=clipboard-generic&utm_source=user_sharing&utm_content=track-29373181&deferredFl=1")]
        [InlineData("https://deezer.page.link/27ms3uwmnFK63wsQ6", "https://www.deezer.com/album/548556802?host=3103984544&utm_campaign=clipboard-generic&utm_source=user_sharing&utm_content=album-548556802&deferredFl=1")]
        [InlineData("https://deezer.page.link/fWTjmEWup6dpU7wU9", "https://www.deezer.com/album/77654472?host=3103984544&utm_campaign=clipboard-generic&utm_source=user_sharing&utm_content=album-77654472&deferredFl=1")]
        public void UnshortenURLTest(string shortened, string expected)
        {
            string unshort = DeezerURL.UnshortenURL(shortened);
            unshort.ShouldBe(expected);
        }

        // it's very possible for the Artist and ArtistTop values to change later
        [Theory]
        [InlineData("https://www.deezer.com/en/track/13787884", new long[] { 13787884 })]
        [InlineData("https://www.deezer.com/en/album/509603971", new long[] { 2531592621, 2531592631, 2531592641, 2531592651, 2531592661 })]
        [InlineData("https://www.deezer.com/en/playlist/12668949561", new long[] { 2664944792, 856599752, 134784964 })]
        [InlineData("https://www.deezer.com/en/artist/11696461", new long[] { 938758732, 938758742, 938758752, 938758762, 938758772, 938758782, 938758792, 938758802, 938758812, 938758822, 140025373 })]
        [InlineData("https://www.deezer.com/en/artist/11696461/top_track", new long[] { 140025373 })]
        public async Task GetAssociatedTracksTest(string url, long[] associatedTracks)
        {
            DeezerClient cli = new();
            var data = DeezerURL.Parse(url);

            long[] actualTracks = await data.GetAssociatedTracks(cli);

            actualTracks.ShouldBeEquivalentTo(associatedTracks);
        }
    }
}