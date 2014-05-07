using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using SendGrid.BallGame.Web.Attributes.Filters;
using SendGrid.BallGame.Web.Models;

namespace SendGrid.BallGame.Web.Controllers
{
    public class InboundController : ApiController
    {
		// POST api/inbound
		[ValidateInput(false)]
		[ParseEmailObjectFilter]
		public ActionResult Post()
		{
			if (ActionContext == null || ActionContext.ActionArguments == null || ActionContext.ActionArguments["email"] == null || !ActionContext.ActionArguments.ContainsKey("email"))
				goto end;

			var email = ActionContext.ActionArguments["email"] as Email;
			if (email == null) goto end;

			// le logic

		end:
			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}