using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR.LeaveManagement.Identity.Configurations;

public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData(new IdentityUserRole<string>()
            {
                RoleId = "0f8fad5b-d9cb-469f-a165-70867728950e",
                UserId = "d3b8f650-fab1-45d2-b69d-39919bc6e7b6"
            },
            new IdentityUserRole<string>()
            {
                RoleId = "7c9e6679-7425-40de-944b-e07fc1f90ae7",
                UserId = "6d9ee531-09cd-4b0c-aebc-b615e06ac0fa"
            });
    }
}