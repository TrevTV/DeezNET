using Xunit;
using Shouldly;
using Org.BouncyCastle.Crypto.Paddings;

namespace DeezNET.Tests
{
    public class DecryptionTests
    {
        [Theory]
        [InlineData("123123123", "f5bb0c8de146c67b44babbf4e6584cc0")]
        [InlineData("401934282", "32a40aef29a46b3aeb4dab3bd4b7e574")]
        [InlineData("2783963122", "8bc53ab8c63c4316107c6a5da8f81046")]
        [InlineData("29373181", "bf77c79ea048067e687e1897d9da3716")]
        [InlineData("89770389", "7468adc148d4229ae4f6268b34d5f661")]
        [InlineData("1233123", "dcb82435c8525869fd04b7214118c3d2")]
        [InlineData("fp0rM4YCTq", "fb19528f5a05858e3cddceb695a0f728")]
        [InlineData("NvxrpLTVjk", "703e3712b65e9ce4057ed517a5c3c96a")]
        [InlineData("DLftoQ96hA", "0ca6e4caf5dbae79b1869e29c5472369")]
        [InlineData("01w014DmrN", "38d8ee9ae51e2608d4113072723ae06e")]
        [InlineData("ObH1gz2EQW", "c8e986bfebfa74013e773f98830ac2d8")]
        [InlineData("GIjrkSADPY", "3c5870e0ce0b2302cc4030966df6ec56")]
        public void CalculateMD5Test(string input, string output)
        {
            Decryption.CalculateMD5(input).ShouldBe(output);
        }

        [Theory]
        [InlineData("123123123", "55eog9)30}whn;5c")]
        [InlineData("401934282", "1d0<d;!gfwuej9ed")]
        [InlineData("2783963122", "nf1:08 ?2t#=<md1")]
        [InlineData("29373181", "3je>g7w15s&?:ogb")]
        [InlineData("89770389", "545bfj,07vvgmjna")]
        [InlineData("1233123", "e37`e;vggsrloe3:")]
        public void GenerateBlowfishKeyTest(string input, string key)
        {
            Decryption.GenerateBlowfishKey(input).ShouldBe(key);
        }

        [Fact]
        public void DecryptChunkTest()
        {
            // Data from FLAC "Paintings" by From Indian Lakes, track ID 79701574
            byte[] cryptedTrackData = [0x51, 0x8B, 0x7F, 0x23, 0x23, 0xEC, 0xC7, 0x08, 0x45, 0x40, 0xF2, 0xD0, 0x33, 0xCC, 0x45, 0x61];
            byte[] decrypted = Decryption.DecryptChunk(Decryption.GenerateBlowfishKey("79701574"), cryptedTrackData);
            decrypted[0..4].ShouldBeEquivalentTo("fLaC"u8.ToArray());
        }

        [Fact]
        public void DecodeTrackStreamTest()
        {
        }
    }
}