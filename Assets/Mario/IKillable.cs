/// <summary>
/// Interface to implement in objects that can be killed
/// </summary>
public interface IKillable
{
    /// <summary>
    /// If something damages an object with this interface, it can call this method.
    /// Allows for custom life/death handling for the target.
    /// </summary>
    public void Kill();
}
