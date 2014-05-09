using System.Data.Entity;

namespace SendGrid.BallGame.Web.Models
{
	public class Context : DbContext
	{
		public DbSet<CommandEntity> Commands { get; set; }
	}
}