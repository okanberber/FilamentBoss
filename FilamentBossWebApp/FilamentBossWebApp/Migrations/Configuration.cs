namespace FilamentBossWebApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Linq.Expressions;
    using FilamentBossWebApp.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<FilamentBossDbModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(FilamentBossDbModel context)
        {
            #region Manager Types
            //context.ManagerRoles.AddOrUpdate(x=> x.ID,new ManagerRole() { ID = 1,Name = "Bronze", IsDeleted = false});
            //context.ManagerRoles.AddOrUpdate(x=> x.ID,new ManagerRole() { ID = 2,Name = "Silver", IsDeleted = false});
            //context.ManagerRoles.AddOrUpdate(x=> x.ID,new ManagerRole() { ID = 3,Name = "Gold", IsDeleted = false});
            #endregion

            #region Manager
            //context.Managers.AddOrUpdate(x => x.ID, new Manager() { ID = 1,ManagerRole_ID=1,Supplier_ID=1, Name = "bronze", Surname = "bronze", Mail = "bronze@bronze", ManagerRole_ID =1,Password="12345", IsActive = true,IsDeleted=false });
            //context.Managers.AddOrUpdate(x => x.ID, new Manager() { ID = 2,ManagerRole_ID=2,Supplier_ID=2, Name = "silver", Surname = "silver", Mail = "silver@silver", ManagerRole_ID =1,Password="12345", IsActive = true,IsDeleted=false });
            //context.Managers.AddOrUpdate(x => x.ID, new Manager() { ID = 3,ManagerRole_ID=3, Supplier_ID = 3, Name = "gold", Surname = "gold", Mail = "gold@gold", ManagerRole_ID =1,Password="12345", IsActive = true,IsDeleted=false });
            #endregion
            #region Supplier
            //context.Suppliers.AddOrUpdate(x => x.ID, new Supplier() { ID = 1, Name = "Bronze",IsDeleted=false });
            //context.Suppliers.AddOrUpdate(x => x.ID, new Supplier() { ID = 2, Name = "Silver",IsDeleted=false });
            //context.Suppliers.AddOrUpdate(x => x.ID, new Supplier() { ID = 3, Name = "Gold",IsDeleted=false });
            #endregion
        }
    }
}
