namespace RuntimeVisualScripting.Data
{
    public interface ILinkable
    {
        void LinkOneWay(ILinkable other);
        void LinkTwoWay(ILinkable other);
        void UnlinkOneWay(ILinkable other);
        void UnlinkTwoWay(ILinkable other);
        ILinkable GetTarget(int index);
        bool IsInputVaraible { get; }
        bool HasLink { get; }
    }
}