namespace MV.Interfaces
{
    public interface IMetaVerseContext
    {
        /// <summary>
        /// Publishes some informations to the verse using <see cref="IElement"/> form
        /// </summary>
        /// <param name="form"></param>
        void Show(IElement form);
    }
}