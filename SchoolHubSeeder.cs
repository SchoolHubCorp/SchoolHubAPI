using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolHubApi.Data;
using SchoolHubApi.Models.Domain;

namespace SchoolHubApi
{
    public class SchoolHubSeeder
    {
        private readonly ApplicationDbContext _dbContext;

        public SchoolHubSeeder(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (_dbContext.Database.IsRelational())
                {
                    var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                    if (pendingMigrations != null && pendingMigrations.Any())
                    {
                        _dbContext.Database.Migrate();
                    }
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
            var roles = new List<Role>()
            {
                new Role()
                {
                    RoleName = "Pupil"
                },
                new Role()
                {
                RoleName = "Parent"
                },
                new Role()
                {
                    RoleName = "Admin"
                },
                new Role()
                {
                    RoleName = "Teacher"
                },
            };

            return roles;
        }
    }
}