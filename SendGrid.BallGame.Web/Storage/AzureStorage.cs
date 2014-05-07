using Microsoft.WindowsAzure.Storage;

namespace SendGrid.BallGame.Web.Storage
{
	public class AzureStorage
	{
		public TableStorage TableStorage { get; private set; }

		public CloudStorageAccount CloudStorageAccount { get; private set; }

		public AzureStorage()
		{
			const string connectionString = "UseDevelopmentStorage=true";
			CloudStorageAccount = CloudStorageAccount.Parse(connectionString);

			TableStorage = new TableStorage(CloudStorageAccount);
		}
	}
}
