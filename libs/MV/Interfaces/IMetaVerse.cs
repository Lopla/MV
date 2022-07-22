namespace MV.Interfaces
{
    public interface IMetaVerse
    {
        void Show(IElement form);
        Task Start();
        Task Init();
    }
}