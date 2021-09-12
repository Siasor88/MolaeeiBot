namespace ConsoleBot.Processors
{
    public interface IProcessor<T>
    {
        public void Process(T obj);
    }
}