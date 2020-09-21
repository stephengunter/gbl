using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
{
	public class ATestsController : BaseApiController
	{
		

		[HttpGet]
		public ActionResult Index()
		{
			return Ok("test");
		}


	}

}