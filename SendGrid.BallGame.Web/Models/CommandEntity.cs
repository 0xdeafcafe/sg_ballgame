using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace SendGrid.BallGame.Web.Models
{
	public enum Command
	{
		Up = 0x00,
		Down = 0x01,
		Left = 0x02,
		Right = 0x03,
		Bieber = 0x04,
		Cyrus = 0x05
	}

	public class CommandEntity : TableEntity
	{
		public static string RowKeyString = "{0}";
		public static string PartitionKeyString = "CommandEntities";

		/// <summary>
		/// yolo rite
		/// </summary>
		public CommandEntity() { }

		/// <summary>
		/// Creates a new Command Entity
		/// </summary>
		/// <param name="command">The command to store in the entity</param>
		public CommandEntity(Command command)
		{
			Command = command;
			Used = false;

			// Set our Partition and Row Keys
			SetKeys();
		}

		/// <summary>
		/// Indicates if the command has been used
		/// </summary>
		public bool Used { get; set; }

		/// <summary>
		/// The integer representation of the command
		/// </summary>
		public int CommandFake { get; set; }

		/// <summary>
		/// The command direction
		/// </summary>
		public Command Command
		{
			get { return (Command)CommandFake; }
			set { CommandFake = (int)value; }
		}

		/// <summary>
		/// Set the entities keys
		/// </summary>
		private void SetKeys()
		{
			PartitionKey = PartitionKeyString;
			RowKey = String.Format(RowKeyString, Guid.NewGuid());
		}
	}
}