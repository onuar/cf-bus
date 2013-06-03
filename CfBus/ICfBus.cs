namespace CfBus
{
    public interface ICfBus
    {
        void Start();

        void Host<TBusinessContract>(TBusinessContract businessInstance);
    }
}