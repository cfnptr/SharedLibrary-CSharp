// =============================================
// Thanks to Dmitrij Kovaliov for such a concept
// =============================================

namespace InjectorGames.SharedLibrary.Entities
{
    /// <summary>
    /// Identifiable class interface
    /// </summary>
    public interface IIdentifiable<T>
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        T ID { get; set; }
    }
}
