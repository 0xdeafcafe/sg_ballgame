using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using SendGrid.BallGame.Web.Models;

namespace SendGrid.BallGame.Web.Controllers
{
	public class CommandController : ApiController
	{
		// GET api/command
		public ActionResult Get()
		{
			var context = new Context();

			// Get un-expired commands, set them as expired, return JSON list
			var commands = context.Commands.Where(c => !c.Used).ToList();
			if (!commands.Any()) return new JsonResult { Data = new List<CommandEntity>() };

			Debug.WriteLine("[GET] api/command :: called :: has pending commands");

			// Set the commands as used in the 
			foreach (var command in commands)
				command.Used = true;
			context.SaveChanges();

			Debug.WriteLine("[GET] api/command :: called :: has pending commands :: {0} commands returned to client", commands.Count());
			return new JsonResult { Data = commands };
		}
	}
}
