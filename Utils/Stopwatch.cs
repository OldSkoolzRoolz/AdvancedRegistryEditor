// Project Name: RegistryEditor
// File Name: Stopwatch.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




#region

using System;

using SW = System.Diagnostics.Stopwatch;

#endregion



namespace Windows.RegistryEditor.Utils;


public class Stopwatch
{


    private readonly SW watch;






    public Stopwatch()
    {
        watch = new SW();
    }






    public TimeSpan Elapsed => watch.Elapsed;

    public bool IsRunning => watch.IsRunning;






    protected virtual void OnStarted()
    {
        Started?.Invoke();
    }






    protected virtual void OnStopped(TimeSpan e)
    {
        Stopped?.Invoke(e);
    }






    public Stopwatch Start(bool reset = false)
    {
        if (reset) watch.Reset();

        watch.Start();
        OnStarted();

        return this;
    }






    public event Action Started;






    public TimeSpan Stop()
    {
        if (!watch.IsRunning)
            throw new InvalidOperationException("Stopwatch isn't running");

        watch.Stop();
        OnStopped(Elapsed);

        return Elapsed;
    }






    public event Action<TimeSpan> Stopped;

}