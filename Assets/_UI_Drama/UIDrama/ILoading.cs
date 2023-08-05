using System;

public interface ILoadable
{
    Action<int> OnProgressChange { get; set; }
}