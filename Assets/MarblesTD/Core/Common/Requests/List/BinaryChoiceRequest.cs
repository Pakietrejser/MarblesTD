namespace MarblesTD.Core.Common.Requests.List
{
    public readonly struct BinaryChoiceRequest : IRequest<bool>
    {
        public readonly string Title;
        public readonly string Description;
        public readonly bool AlwaysTrue;

        public BinaryChoiceRequest(string title, string description, bool alwaysTrue = false)
        {
            Title = title;
            Description = description;
            AlwaysTrue = alwaysTrue;
        }
    }
}