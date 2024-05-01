namespace DeezNET.Metadata;

// https://stackoverflow.com/questions/14959320/taglib-sharp-file-from-bytearray-stream#31032997
internal class FileBytesAbstraction : TagLib.File.IFileAbstraction
{
    public FileBytesAbstraction(string name, byte[] bytes)
    {
        Name = name;

        // copying it like this allows for an expandable stream
        //using MemoryStream arrayStream = new(bytes);
        MemoryStream stream = new();
        stream.Write(bytes, 0, bytes.Length);
        //arrayStream.CopyTo(stream);
        
        MemoryStream = stream;
    }

    public void CloseStream(Stream stream)
    {
        // shared read/write stream so we don't want it to close it
    }

    public string Name { get; private set; }

    public Stream ReadStream { get => MemoryStream; }

    public Stream WriteStream { get => MemoryStream; }

    public MemoryStream MemoryStream { get; private set; }
}