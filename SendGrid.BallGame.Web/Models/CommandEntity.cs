using System.ComponentModel.DataAnnotations;

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

	public class CommandEntity
	{
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
		}

		/// <summary>
		/// 
		/// </summary>
		[Key]
		public int Id { get; set; }

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
	}
}