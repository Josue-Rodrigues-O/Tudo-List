namespace Tudo_List.Domain.Core.Interfaces.Validation
{
    public interface IUserValidator
    {
        IUserValidator WithName(string name);
        IUserValidator WithEmail(string name);
        IUserValidator WithPassword(string name);
        void Validate();
    }
}
