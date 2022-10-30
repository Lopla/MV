namespace MV.Interfaces
{
    public interface IMetaVerseContext
    {
        /// <summary>
        ///     Publishes some information to the verse using <see cref="IElement" /> form
        /// </summary>
        void Show(IElement element);

        void Hide(IElement element);

        void Update(IElement element);
    }
}