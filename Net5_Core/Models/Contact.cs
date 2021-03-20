using System;
using System.ComponentModel.DataAnnotations;

namespace Net5_Core.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required, StringLength(100, ErrorMessage = "Name value cannot exceed 4 characters. ")]
        public string Name { get; set; }

        [Required, StringLength(100, ErrorMessage = "Email value cannot exceed 4 characters. ")]
        public string Email { get; set; }

        [StringLength(75, ErrorMessage = "Company value cannot exceed 4 characters. ")]
        public string Company { get; set; }

        [StringLength(50, ErrorMessage = "Country value cannot exceed 4 characters. ")]
        public string Country { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }


        public Contact()
        {
        }

        public void Update(Contact contact)
        {
            if (contact == null)
                throw new Exception("Update contact details empty.");

            contact.Trim();

            Name = contact.Name;
            Email = contact.Email;
            Company = contact.Company;
            Country = contact.Country;
            Modified = DateTime.Now;

            this.Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ClientError("User name is required.");

            if (string.IsNullOrEmpty(Email))
                throw new ClientError("User email is required.");
        }


        public void Trim()
        {
            Name = Name?.Trim();
            Email = Email?.Trim();
            Company = Company?.Trim();
            Country = Country?.Trim();
        }
    }
}
