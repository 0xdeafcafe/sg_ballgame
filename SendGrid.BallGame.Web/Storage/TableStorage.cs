using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace SendGrid.BallGame.Web.Storage
{
	public class TableStorage
	{
		public CloudTableClient TableClient { get; private set; }

		public CloudStorageAccount CloudStorageAccount { get; private set; }

		public TableStorage(CloudStorageAccount cloudStorageAccount)
		{
			CloudStorageAccount = cloudStorageAccount;
			TableClient = CloudStorageAccount.CreateCloudTableClient();

			// Create Audio Table
			BallGameCloudTable = TableClient.GetTableReference("BallGame");
			BallGameCloudTable.CreateIfNotExists();
		}

		public CloudTable BallGameCloudTable { get; private set; }

		/// <summary>
		/// Insert or Replace a single <see cref="ITableEntity"/> into an Azure Table
		/// </summary>
		/// <param name="tableEntity">The <see cref="ITableEntity"/> to insert or replace.</param>
		public void InsertOrReplaceSingleEntity(ITableEntity tableEntity)
		{
			try
			{
				var insertOrReplaceOperation = TableOperation.InsertOrReplace(tableEntity);
				BallGameCloudTable.Execute(insertOrReplaceOperation);
			}
			catch (Exception ex)
			{
				Trace.TraceError(ex.ToString());
			}
		}

		/// <summary>
		/// Retrieves a Enumerable list of <see cref="ITableEntity"/>s.
		/// </summary>
		/// <param name="partitionKey">The Partition Key of the <see cref="ITableEntity"/>'s to return.</param>
		/// <returns></returns>
		public IEnumerable<TEntityType> RetrieveMultipleEntities<TEntityType>(string partitionKey, string query)
			where TEntityType : ITableEntity, new()
		{
			var rangeQuery = new TableQuery<TEntityType>().Where(
				TableQuery.CombineFilters(
					TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
					TableOperators.And,
					query));
			return BallGameCloudTable.ExecuteQuery(rangeQuery).ToList();
		}
	}
}