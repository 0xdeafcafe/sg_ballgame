using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage.Table;
using SendGrid.BallGame.Web.Models;
using SendGrid.BallGame.Web.Storage;

namespace SendGrid.BallGame.Web.Controllers
{
	public class CommandController : ApiController
	{
		// GET api/command
		public ActionResult Get()
		{
			var storage = new AzureStorage();

			// Get un-expired commands, set them as expired, return JSON list
			var commands = storage.TableStorage.RetrieveMultipleEntities<CommandEntity>(CommandEntity.PartitionKeyString,
				TableQuery.GenerateFilterConditionForBool("Used", QueryComparisons.Equal, false));

			var commandEntities = commands as CommandEntity[] ?? commands.ToArray();
			if (!commandEntities.Any()) return new JsonResult { Data = new List<CommandEntity>() };

			Debug.WriteLine("[GET] api/command :: called :: has pending commands");

			// Set the commands as used in the 
			foreach (var command in commandEntities)
			{
				var newCommand = command;
				newCommand.Used = true;
				storage.TableStorage.InsertOrReplaceSingleEntity(newCommand);
			}

			Debug.WriteLine("[GET] api/command :: called :: has pending commands :: {0} commands returned to client", commandEntities.Count());

			return new JsonResult { Data = commands };
		}
	}
}
