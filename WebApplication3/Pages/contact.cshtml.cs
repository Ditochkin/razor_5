using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesGeneral.Pages
{
    public class contactModel : PageModel
    {
        private readonly IContactsService contactsService;

        public contactModel(IContactsService service)
        {
            this.contactsService = service;
        }

        public void OnPost()
        {
            var newContact = new Contact(Request.Form["first_name"],
                                         Request.Form["last_name"],
                                         Request.Form["email"],
                                         Request.Form["phone"],
                                         Request.Form["select_service"],
                                         Request.Form["select_price"],
                                         Request.Form["comments"]);

            contactsService.writeContact(newContact);
        }

        public void OnGet()
        {
        }
    }
}
