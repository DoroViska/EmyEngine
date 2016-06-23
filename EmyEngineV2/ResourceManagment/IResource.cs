using System.IO;

namespace EmyEngine.ResourceManagment
{
    public interface IResource
    {
        byte[] Data { get; set; }
        string Path { get; }  
    }
}