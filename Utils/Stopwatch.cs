// Project Name: AdvancedRegistryEditor
// Adapted  and expanded from https://github.com/giladreich/RegistryEditor
// File Name: Stopwatch.cs
// Author:  Kyle Crowder
// Github:  OldSkoolzRoolz
// Distributed under Open Source License
// Do not remove file headers




using System;

using SW = System.Diagnostics.Stopwatch;



namespace Windows.RegistryEditor.Utils;
/// <summary>
///     Provides a wrapper around <see cref="System.Diagnostics.Stopwatch" /> with event support for start and stop
///     actions.
/// </summary>
public class Stopwatch
{

    private readonly SW watch;






    /// <summary>
    ///     Initializes a new instance of the <see cref="Stopwatch" /> class.
    /// </summary>
    public Stopwatch()
    {
        watch = new SW();
    }






    /// <summary>
    ///     Gets the total elapsed time measured by the current instance.
    /// </summary>
    public TimeSpan Elapsed => watch.Elapsed;

    /// <summary>
    ///     Gets a value indicating whether the <see cref="Stopwatch" /> is currently running.
    /// </summary>
    public bool IsRunning => watch.IsRunning;






    /// <summary>
    ///     Raises the <see cref="Started" /> event.
    /// </summary>
    protected virtual void OnStarted()
    {
        Started?.Invoke();
    }






    /// <summary>
    ///     Raises the <see cref="Stopped" /> event.
    /// </summary>
    /// <param name="e">The elapsed time when the stopwatch was stopped.</param>
    protected virtual void OnStopped(TimeSpan e)
    {
        Stopped?.Invoke(e);
    }






    /// <summary>
    ///     Starts the stopwatch, optionally resetting the elapsed time.
    /// </summary>
    /// <param name="reset">If <c>true</c>, resets the elapsed time before starting.</param>
    /// <returns>The current <see cref="Stopwatch" /> instance.</returns>
    public Stopwatch Start(bool reset = false)
    {
        if (reset)
        {
            watch.Reset();
        }

        watch.Start();
        OnStarted();

        return this;
    }






    /// <summary>
    ///     Occurs when the stopwatch is started.
    /// </summary>
    public event Action Started;






    /// <summary>
    ///     Stops the stopwatch and raises the <see cref="Stopped" /> event.
    /// </summary>
    /// <returns>The elapsed time when the stopwatch was stopped.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the stopwatch is not running.</exception>
    public TimeSpan Stop()
    {
        if (!watch.IsRunning)
        {
            throw new InvalidOperationException("Stopwatch isn't running");
        }

        watch.Stop();
        OnStopped(Elapsed);

        return Elapsed;
    }






    /// <summary>
    ///     Occurs when the stopwatch is stopped.
    /// </summary>
    public event Action<TimeSpan> Stopped;

}