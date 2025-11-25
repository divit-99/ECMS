using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECMS.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedCompaniesEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "ID", "CompanyName", "Domain", "Industry", "Website" },
                values: new object[,]
                {
                    { 1, "Alpha Corp", "alpha.com", "Technology", "https://alpha.com" },
                    { 2, "Beta Ltd", "beta.org", "Finance", "https://beta.org" },
                    { 3, "Gamma Inc", "gamma.net", "Healthcare", "https://gamma.net" },
                    { 4, "Delta LLC", "delta.io", "Retail", "https://delta.io" },
                    { 5, "Epsilon GmbH", "epsilon.de", "Manufacturing", "https://epsilon.de" }
                }
            );

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "ID", "Name", "Email", "Phone", "JobTitle", "CompanyID", "IsActive", "CreatedAt" },
                values: new object[,]
                {
                    { 1, "Alice Smith", "alice@alpha.com", "555-0100", "Software Engineer", 1, true, DateTime.UtcNow },
                    { 2, "Bob Jones", "bob@alpha.com", "555-0101", "QA Analyst", 1, true, DateTime.UtcNow },

                    { 3, "Carol White", "carol@beta.org", "555-0102", "Accountant", 2, true, DateTime.UtcNow },
                    { 4, "David Brown", "david@beta.org", "555-0103", "Financial Analyst", 2, true, DateTime.UtcNow },

                    { 5, "Eve Davis", "eve@gamma.net", "555-0104", "Nurse", 3, true, DateTime.UtcNow },
                    { 6, "Frank Moore", "frank@gamma.net", "555-0105", "Doctor", 3, true, DateTime.UtcNow },

                    { 7, "Grace Wilson", "grace@delta.io", "555-0106", "Sales Manager", 4, true, DateTime.UtcNow },
                    { 8, "Henry Taylor", "henry@delta.io", "555-0107", "Store Manager", 4, true, DateTime.UtcNow },
                    { 9, "Nate Lewis", "nate@delta.io", "555-0113", "Customer Service", 4, true, DateTime.UtcNow },

                    { 10, "Ivy Anderson", "ivy@epsilon.de", "555-0108", "Production Supervisor", 5, true, DateTime.UtcNow },
                    { 11, "Jack Thomas", "jack@epsilon.de", "555-0109", "Engineer", 5, true, DateTime.UtcNow },
                    { 12, "Olivia Walker", "olivia@epsilon.de", "555-0114", "Quality Inspector", 5, true, DateTime.UtcNow },

                    { 13, "Kara Martin", "kara@alpha.com", "555-0110", "HR Specialist", 1, true, DateTime.UtcNow },
                    { 14, "Leo Harris", "leo@beta.org", "555-0111", "Tax Consultant", 2, true, DateTime.UtcNow },
                    { 15, "Mona Clark", "mona@gamma.net", "555-0112", "Lab Technician", 3, true, DateTime.UtcNow }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Employees", "ID", Enumerable.Range(1, 15).ToArray());
            migrationBuilder.DeleteData("Companies", "ID", Enumerable.Range(1, 5).ToArray());
        }
    }
}
