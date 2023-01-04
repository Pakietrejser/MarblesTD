namespace MarblesTD.Core.Common.Requests.List
{
    public readonly struct PurchaseRequest : IRequest<bool>
    {
        public readonly int RequiredHoney;

        public PurchaseRequest(int requiredHoney)
        {
            RequiredHoney = requiredHoney;
        }
    }
}