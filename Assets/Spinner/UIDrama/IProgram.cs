namespace UIDrama
{
    public interface IProgram
    {
        bool IsRunning { get; set; }
        void Run();
        void Stop();
    }
}