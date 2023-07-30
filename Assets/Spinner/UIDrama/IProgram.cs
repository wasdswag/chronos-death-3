using UnityEngine;

namespace UIDrama
{
    public interface IProgram
    {
        GameObject[] UIDramaElements { get; set; }
        bool IsRunning { get; set; }
        void Run();
        void Stop();
    }
}