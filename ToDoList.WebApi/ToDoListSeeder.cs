using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ToDoList.WebApi.Core.Models.Domains;
using ToDoList.WebApi.Persistence;

namespace ToDoList.WebApi
{
    public class ToDoListSeeder
    {
        private readonly ToDoListContext _dbContext;

        public ToDoListSeeder(ToDoListContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }

                if (!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }
            }

        }

        private IEnumerable<Role> GetRoles()
        {

            var roleNames = (RoleName[])Enum.GetValues(typeof(RoleName));

            var roles = roleNames
                .Select(rn => new Role
                {
                    Name = rn.ToString()
                })
                .ToList();

            return roles;
        }

    }
}
