namespace Bobkov.BL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succeedeed, string message, string category)
        {
            Succeedeed = succeedeed;
            Message = message;
            Category = category;
        }
        public bool Succeedeed { get; private set; }
        public string Message { get; private set; }
        public string Category { get; private set; }
    }
}
