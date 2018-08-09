namespace EventTracker.Models
{
    public interface IUserModel
    {
        string UserEmail { get; set; }
        int UserId { get; }
        string UserPassword { get; set; }
    }
}