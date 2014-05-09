namespace SendGrid.BallGame.Web.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class CreatedCommandEntityModel : DbMigration
	{
		public override void Up()
		{
			CreateTable(
				"dbo.CommandEntities",
				c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Used = c.Boolean(nullable: false),
						CommandFake = c.Int(nullable: false),
						Command = c.Int(nullable: false),
					})
				.PrimaryKey(t => t.Id);

		}

		public override void Down()
		{
			DropTable("dbo.CommandEntities");
		}
	}
}
