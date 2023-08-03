using UnityEngine;

namespace UIDrama
{
    public interface IFile
    {
        string Filename { get; }
        Vector2 Position { get; }
    }
}