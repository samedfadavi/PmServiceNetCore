using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Moq;
using Newtonsoft.Json.Linq;
using pmService.Controllers;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Controllers
{
    public class DerakhtTajhizatControllerTests
    {
        private readonly Mock<ISqlDataAccess> _sqlMock;
        private readonly Mock<IProcedureDataAccess> _procedureMock;
        private readonly Derakht_TajhizatController _controller;

        public DerakhtTajhizatControllerTests()
        {
            // Mock SQL data access
            var fakeResult = new List<Dictionary<string, object>>
{
    new Dictionary<string, object>
    {
        { "ID", 1 },
        { "Name", "Item1" }
    }
};

            _sqlMock = new Mock<ISqlDataAccess>();
            _sqlMock
                .Setup(s => s.ExecuteSqlAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                .ReturnsAsync(fakeResult); // <-- Matches the exact return type
            // Mock procedure data access
            _procedureMock = new Mock<IProcedureDataAccess>();
            _procedureMock
                .Setup(p => p.ExecuteProcedureAsync(It.IsAny<string>(), It.IsAny<SqlParameter[]>()))
                .ReturnsAsync(CreateFakeDataTable());

            // Inject mocks into controller
            _controller = new Derakht_TajhizatController(
                _sqlMock.Object,
                _procedureMock.Object
            );
        }

        [Fact]
        public async Task List_Iradat_ReturnsOkResult_WithExpectedData()
        {
            // Arrange: valid table/column names
            var items = new JArray
            {
                new JObject
                {
                    { "Name_Jadval_Pm", "Tbl_Tajhiz_A" },
                    { "Name_Pm", "Name" },
                    { "Code_Pm", "Code" },
                    { "ID", 1 }
                }
            };

            // Act
            var result = await _controller.List_Iradat(items);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var table = Assert.IsType<DataTable>(okResult.Value);

            Assert.Single(table.Rows);
            Assert.Equal("Item1", table.Rows[0]["Name"]);
        }

        [Fact]
        public async Task List_Iradat_ThrowsSecurityException_OnInvalidTable()
        {
            // Arrange: invalid table
            var items = new JArray
            {
                new JObject
                {
                    { "Name_Jadval_Pm", "InvalidTable" },
                    { "Name_Pm", "Name" },
                    { "Code_Pm", "Code" },
                    { "ID", 1 }
                }
            };

            // Act + Assert
            await Assert.ThrowsAsync<SecurityException>(
                () => _controller.List_Iradat(items)
            );
        }

        [Fact]
        public async Task List_Iradat_ThrowsSecurityException_OnInvalidColumn()
        {
            // Arrange: invalid column
            var items = new JArray
            {
                new JObject
                {
                    { "Name_Jadval_Pm", "Tbl_Tajhiz_A" },
                    { "Name_Pm", "InvalidColumn" },
                    { "Code_Pm", "Code" },
                    { "ID", 1 }
                }
            };

            // Act + Assert
            await Assert.ThrowsAsync<SecurityException>(
                () => _controller.List_Iradat(items)
            );
        }

        // Helper: fake DataTable returned by procedure
        private static DataTable CreateFakeDataTable()
        {
            var table = new DataTable();
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));

            table.Rows.Add(1, "Item1");
            return table;
        }
    }
}
