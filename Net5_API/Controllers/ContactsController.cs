using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Net5_Core.Interfaces;
using Net5_Core.Models;
using Net5_Core.UseCases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net5_API.Controllers
{
    [Route("api/[controller]"), ApiController]
    public class ContactsController : ControllerBase
    {
        //Alternate approach to resolve dependencies, more control less clean

        //private readonly IServiceProvider _serviceProvider;

        //public ContactController(IServiceProvider serviceProvider)
        //{
        //    _serviceProvider = serviceProvider;
        //}


        //[HttpGet, Route("GetAll")]
        //public List<Contact> GetAll()
        //{
        //    var efUOW = _serviceProvider.GetService<IEFUoW>();
        //    var useCase = new GetAllContacts(efUOW);
        //    return useCase.Execute();
        //}


        private readonly IEFUoW _efUoW;
        private readonly ILogger _logger;

        public ContactsController(IEFUoW efUoW, ILogger logger)
        {
            _efUoW = efUoW;
            _logger = logger;
        }


        [HttpGet, Route("GetAll")]
        public ActionResult<List<Contact>> GetAll()
        {
            var useCase = new GetAllContacts(_efUoW);
            return Ok(useCase.Execute());
        }

        [HttpPost, Route("Add")]
        public async Task<ActionResult<string>> Add(Contact contact)
        {
            var useCase = new AddNewContact(_efUoW);
            return Ok(await useCase.Execute(contact));
        }

        [HttpDelete, Route("Delete")]
        public async Task<ActionResult<string>> Delete(int contactId)
        {
            var useCase = new DeleteContact(_efUoW);
            return Ok(await useCase.Execute(contactId));
        }

        [HttpPut, Route("Update")]
        public async Task<ActionResult<string>> Update(Contact contact)
        {
            var useCase = new UpdateContact(_efUoW);
            return Ok(await useCase.Execute(contact));
        }
    }
}
