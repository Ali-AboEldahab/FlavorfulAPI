namespace Flavorful.Core.Entities.Identity
{
    public class UserAddress
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string AppUserId { get; set; } //FK : AppUser

        public static implicit operator UserAddress(Address v)
        {
            throw new NotImplementedException();
        }
    }
}