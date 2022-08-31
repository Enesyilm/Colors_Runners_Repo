namespace Extentions.Interfaces
{
    public interface IState
    {
        void OnSetup();
        void OnEnter();
        void OnExit();
        
    }
}