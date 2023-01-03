namespace RuntimeVisualScripting.Data
{
    public interface ILinkable
    {
        void LinkOneWay(ILinkable other);
        void LinkTwoWay(ILinkable other);
        void UnlinkOneWay(ILinkable other);
        void UnlinkTwoWay(ILinkable other);
        ILinkable GetTarget(int index);
        bool IsInputVariable { get; }
        bool HasLink { get; }
        bool AlreadyConnected(ILinkable other);
    }
}