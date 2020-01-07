namespace Bobkov.BL.Infrastructure
{
    public class OperationDetails
    {
        public OperationDetails(bool succeedeed, string message, string prop)
        {
            Succeedeed = succeedeed;
            Message = message;
            Property = prop;
        }
        public bool Succeedeed { get; private set; }
        public string Message { get; private set; }
        public string Property { get; private set; }
    }
}
