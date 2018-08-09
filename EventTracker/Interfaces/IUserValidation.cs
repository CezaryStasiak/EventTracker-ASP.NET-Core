namespace EventTracker.Models
{
    public interface IUserValidation
    {
        UserValidation RandomError();
        UserValidation UserCreationSuccess();
        UserValidation UserNameTaken();
        UserValidation WrongPassword();
    }
}