using InjectorGames.SharedLibrary.Extensions;
using System.IO;
using System.Numerics;

namespace InjectorGames.SharedLibrary.Games.Players
{
    /// <summary>
    /// Player transform container structure
    /// </summary>
    public struct PlayerTransform
    {
        /// <summary>
        /// Player transform container byte size
        /// </summary>
        public const int ByteSize = VectorExtension.ByteSizeVector3 + VectorExtension.ByteSizeVector2;

        /// <summary>
        /// Player body position in the global world space
        /// </summary>
        public Vector3 bodyPosition;
        /// <summary>
        /// Player head and body rotation in the global world space along Y axis
        /// </summary>
        public Vector2 bodyHeadRotation;

        /// <summary>
        /// Creates a new player transform structure instance
        /// </summary>
        public PlayerTransform(Vector3 bodyPosition, Vector2 bodyHeadRotation)
        {
            this.bodyPosition = bodyPosition;
            this.bodyHeadRotation = bodyHeadRotation;
        }
        /// <summary>
        /// Creates a new player transform structure instance
        /// </summary>
        public PlayerTransform(Vector3 bodyPosition)
        {
            this.bodyPosition = bodyPosition;
            bodyHeadRotation = default;
        }
        /// <summary>
        /// Creates a new player transform structure instance
        /// </summary>
        public PlayerTransform(BinaryReader binaryReader)
        {
            bodyPosition = VectorExtension.ToVector3(binaryReader);
            bodyHeadRotation = VectorExtension.ToVector2(binaryReader);
        }

        /// <summary>
        /// Converts player transform values to the byte array
        /// </summary>
        public void ToBytes(BinaryWriter binaryWriter)
        {
            bodyPosition.ToBytes(binaryWriter);
            bodyHeadRotation.ToBytes(binaryWriter);
        }
    }
}
