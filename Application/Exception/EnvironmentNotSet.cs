namespace Application.Models
{
    [System.Serializable]
    public class EnvironmentNotSet : System.Exception
    {
        public EnvironmentNotSet()
        {
        }

        public EnvironmentNotSet(string message) : base(message)
        {
        }

        public EnvironmentNotSet(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected EnvironmentNotSet(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }
    }
}