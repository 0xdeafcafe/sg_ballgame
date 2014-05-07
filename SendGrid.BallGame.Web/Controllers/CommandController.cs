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

			// Set the commands as used in the 
			foreach (var command in commands)
			{
				var newCommand = command;
				newCommand.Used = true;
				storage.TableStorage.InsertOrReplaceSingleEntity(newCommand);
			}

			return new JsonResult { Data = commands };
		}
	}
}
