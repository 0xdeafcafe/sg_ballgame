using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Mvc;
using SendGrid.BallGame.Web.Attributes.Filters;
using SendGrid.BallGame.Web.Models;
using SendGrid.BallGame.Web.Storage;

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
			CommandEntity commandEntity = null;
			var commandStringValue = Regex.Replace(email.Subject, @"[\W]", "").ToLowerInvariant();
			switch (commandStringValue)
			{
				case "left": commandEntity = new CommandEntity(Command.Left); break;
				case "right": commandEntity = new CommandEntity(Command.Right); break;
				case "up": commandEntity = new CommandEntity(Command.Up); break;
				case "down": commandEntity = new CommandEntity(Command.Down); break;
			}

			if (commandEntity != null)
				new AzureStorage().TableStorage.InsertOrReplaceSingleEntity(commandEntity);

		end:
			return new HttpStatusCodeResult(HttpStatusCode.OK);
		}
	}
}