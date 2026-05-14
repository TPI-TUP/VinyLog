using System.ComponentModel.DataAnnotations;

namespace Application.Models.Requests; 
    public class AutheticationRequest
    {
        [Required]
        public string? Username {get;set;}
        [Required]
        public string? Password {get;set;}
    }


