namespace ConexaoVerde.Web.Extensions;

public static class StreamExtensions
{
    public static async Task<byte[]> ReadToEndAsync(this Stream stream)
    {
        using var memoryStream = new MemoryStream();
        await stream.CopyToAsync(memoryStream);
        return memoryStream.ToArray();
    }
}