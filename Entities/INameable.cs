namespace InjectorGames.SharedLibrary.Entities
{
    /// <summary>
    /// Namebale class interface
    /// </summary>
    public interface INameable<T>
    {
        /// <summary>
        /// Unique name
        /// </summary>
        T Name { get; set; }
    }
}
