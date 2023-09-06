using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Pipelines.Sockets.Unofficial.SocketConnection;

namespace WebApp10
{
    public class Product
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string myname { get; set; }

        public string myname1 { get; set; }
    }


    public class Persons
    {
        public string? Name { get; set; }
        public string? classification { get; set; }
    }


    public class GeneralInfo
    {
        public   List<Clients> clients { get; set; }
        public   List<Fields> fields { get; set; }

        public   List<Countries> countries { get; set; }
    }

    public class Countries
    {
        public string? Name { get; set; }
    }


    public class Clients
    {
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Married { get; set; }
    }

    public class Fields
    {
        public string? Name { get; set; } 
        public string? Age { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Married { get; set; }

        public string? type { get; set; }
         
    }

    public class Account
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<string> Roles { get; set; }
    }

    public class PersonModel
    {
        ///<summary>
        /// Gets or sets PersonId.
        ///</summary>
        public int PersonId { get; set; }

        ///<summary>
        /// Gets or sets Name.
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// Gets or sets Gender.
        ///</summary>
        public string Gender { get; set; }

        ///<summary>
        /// Gets or sets City.
        ///</summary>
        public string City { get; set; }
    }

    public class PersonX
    {
        public string? name { get; set; }
        public string? surname { get; set; }
    }

    public class ClientsJSON
    {
        public List<Clients1> clients = new List<Clients1>();
    }

    public class Clients1
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public int? Country { get; set; }
        public string? Address { get; set; }
        public bool? Married { get; set; }
        
    }


    public class RedisClass
    {
        public string? data1 { get; set; }
        public string? data2 { get; set; }
        public string? data3 { get; set; }
    }


    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class PersonModelX
    {
        public List<RoleModel> Roles { get; set; }
        public string Name { get; set; }
    }


    public class PersonModelY
    {
        public List<RoleModel> Roles = new List<RoleModel>();
        public List<RoleModelY> RolesY = new List<RoleModelY>();
        public List<RoleModel>? RolesGet { get; set; }
        public List<RoleModelY>? RolesGetY { get; set; }
    }


    public class RoleModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
    }

    public class RoleModelY
    {
        public string RoleNameY { get; set; }
        public string DescriptionY { get; set; }
    }

    public class Car
    {
        public string name { get; set; }
        public string year { get; set; }
    }


    public abstract class Person
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }
    }
}