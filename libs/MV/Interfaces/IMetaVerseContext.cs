namespace MV.Interfaces
{
    public interface IMetaVerseContext
    {
        /// <summary>
        /// Publishes some information to the verse using <see cref="IElement"/> form
        /// </summary>
        /// <param name="form"></param>
        void Show(IElement form);
    }
}