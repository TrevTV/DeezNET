using System.Reflection;
using Xunit.Sdk;

namespace DeezNET.Tests;


// modified from https://patriksvensson.se/posts/2017/11/using-embedded-resources-in-xunit-tests
public sealed class EmbeddedResourceDataAttribute(params string[] args) : DataAttribute
{
    private readonly string[] _args = args;

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        var result = new object[_args.Length];
        for (var index = 0; index < _args.Length; index++)
        {
            result[index] = ReadManifestData(_args[index]);
        }
        return new[] { result };
    }

    public static byte[] ReadManifestData(string resourceName)
    {
        var assembly = typeof(EmbeddedResourceDataAttribute).GetTypeInfo().Assembly;
        resourceName = resourceName.Replace("/", ".");
        using var stream = assembly.GetManifestResourceStream(resourceName) ?? throw new InvalidOperationException("Could not load manifest resource stream.");
        using MemoryStream memStream = new();
        stream.CopyTo(memStream);
        return memStream.ToArray();
    }
}
