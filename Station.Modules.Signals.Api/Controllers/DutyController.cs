using Station.Common.Classes;
using Station.Modules.Signals.Domain.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Station.Modules.Signals.Api.Controllers
{
    [Route("api/[controller]")]
    public class SignalController : Controller
    {
        protected ISignalService _SignalService;

        private AppSettings ConfigSettings { get; set; }

        public SignalController(ISignalService SignalService, IOptions<AppSettings> settings)
        {
            _SignalService = SignalService;
            this.ConfigSettings = settings.Value;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(long id)
        {
            return "value";
        }
    }
}
