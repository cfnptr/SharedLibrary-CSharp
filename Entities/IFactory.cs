namespace InjectorGames.SharedLibrary.Entities
{
    /// <summary>
    /// Factory interface
    /// </summary>
    public interface IFactory<T>
    {
        /// <summary>
        /// Creates a new item instance
        /// </summary>
        T Create();
    }
}
