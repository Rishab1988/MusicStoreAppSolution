namespace MusicStoreApp.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Resources;

    public class UserLoginViewModel
    {


        // [Required(ErrorMessageResourceName = "User Name is required", ErrorMessageResourceType = typeof(System.Resources)))]
        [Required(ErrorMessage = "Required")]
        [Display(Name = "User Name",Order =1,Prompt = "User Name")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}