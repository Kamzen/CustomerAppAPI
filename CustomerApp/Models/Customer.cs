

using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Models
{
    public class Customer
    {

        public Guid CustomerID { get; set; }
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string UserName { get; set; }
        //giving me some issues and still have react to setup
        /*private string? _username;
        private int _age;
        public string? Username
        {
            get => _username; set
            {
                _username = $"{FirstName} {LastName}";
            }
        }*/

        /*public string UserName()
        {
            return $"{this.FirstName} {this.LastName}";
        }*/

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

       /* public int Age
        {
            get { return this._age; }
            set { 
                this._age = 2022 - Int32.Parse(DateTime.Parse(DateOfBirth.ToString()).Year.ToString());
            }
        }*/

        public DateTime DateCreated { get; set; }
        public DateTime? DateEdited { get; set; }

        public bool IsDeleted { get; set; } = false;

    }
}
