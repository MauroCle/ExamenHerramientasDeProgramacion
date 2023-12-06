using System.ComponentModel.DataAnnotations;
using Examenes.Models;

namespace Examenes.ViewModels;

public class ClientDetailsViewModel{

    public int Id {get; set;}
    [Display(Name="Nombre")]
    public string FirstName {get; set;} = null!;
    [Display(Name="Apellido")]
    public string LastName {get; set;} = null!;
    [Display(Name="Email")]
    public string Email {get; set;} = null!;
    [Display(Name="Numero Telefonico")]
   public string PhoneNumber {get; set;} = null!;
   [Display(Name="Direcciones")]
    public List<Address> Addresses {get; set;} = new List<Address>(); //TODO esto podria ser una lista de AddressDetailViewModel

}