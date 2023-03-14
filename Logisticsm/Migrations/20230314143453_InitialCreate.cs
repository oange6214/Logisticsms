using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logisticsm.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    telphone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    address = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    description = table.Column<string>(type: "ntext", nullable: true),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tag = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ExpressTransport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: false),
                    customerid = table.Column<int>(name: "customer_id", type: "int", nullable: false),
                    senddate = table.Column<DateTime>(name: "send_date", type: "datetime", nullable: false),
                    ordernumber = table.Column<string>(name: "order_number", type: "nvarchar(64)", maxLength: 64, nullable: false),
                    channel = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    volume = table.Column<double>(type: "float", nullable: false),
                    weight = table.Column<double>(type: "float", nullable: false),
                    costweight = table.Column<double>(name: "cost_weight", type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    sourceplace = table.Column<string>(name: "source_place", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    targetplace = table.Column<string>(name: "target_place", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tag = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ExpressTransportDetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: false),
                    expresstransportid = table.Column<int>(name: "express_transport_id", type: "int", nullable: false),
                    productor = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    receivedate = table.Column<DateTime>(name: "receive_date", type: "datetime", nullable: false),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tag = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    level = table.Column<int>(type: "int", nullable: false),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tag = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AirTransport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: true),
                    customerid = table.Column<int>(name: "customer_id", type: "int", nullable: true),
                    senddate = table.Column<DateTime>(name: "send_date", type: "datetime", nullable: true),
                    ordernumber = table.Column<string>(name: "order_number", type: "nvarchar(64)", maxLength: 64, nullable: true),
                    batch = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    sourceplace = table.Column<string>(name: "source_place", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    targetplace = table.Column<string>(name: "target_place", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tag = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirTransport", x => x.id);
                    table.ForeignKey(
                        name: "FK_AirTransport_Customer",
                        column: x => x.customerid,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SeaTransport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: false),
                    customerid = table.Column<int>(name: "customer_id", type: "int", nullable: false),
                    senddate = table.Column<DateTime>(name: "send_date", type: "datetime", nullable: true),
                    boxmodel = table.Column<string>(name: "box_model", type: "nvarchar(64)", maxLength: 64, nullable: true),
                    boxnumber = table.Column<string>(name: "box_number", type: "nvarchar(64)", maxLength: 64, nullable: true),
                    batch = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    count = table.Column<int>(type: "int", nullable: true),
                    volume = table.Column<double>(type: "float", nullable: true),
                    weight = table.Column<double>(type: "float", nullable: true),
                    sourceplace = table.Column<string>(name: "source_place", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    targetplace = table.Column<string>(name: "target_place", type: "nvarchar(50)", maxLength: 50, nullable: true),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tag = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeaTransport", x => x.id);
                    table.ForeignKey(
                        name: "FK_SeaTransport_Customer",
                        column: x => x.customerid,
                        principalTable: "Customer",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AirTransportDetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    airtransportid = table.Column<int>(name: "air_transport_id", type: "int", nullable: true),
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: true),
                    receivedate = table.Column<DateTime>(name: "receive_date", type: "datetime", nullable: true),
                    count = table.Column<int>(type: "int", nullable: true),
                    weight = table.Column<double>(type: "float", nullable: true),
                    volume = table.Column<double>(type: "float", nullable: true),
                    length = table.Column<double>(type: "float", nullable: true),
                    width = table.Column<double>(type: "float", nullable: true),
                    height = table.Column<double>(type: "float", nullable: true),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    tag = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirTransportDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_AirTransportDetail_AirTransport",
                        column: x => x.airtransportid,
                        principalTable: "AirTransport",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SeaTransportDetail",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    seatransportid = table.Column<int>(name: "sea_transport_id", type: "int", nullable: false),
                    memberid = table.Column<int>(name: "member_id", type: "int", nullable: false),
                    receivedate = table.Column<DateTime>(name: "receive_date", type: "datetime", nullable: true),
                    productor = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    count = table.Column<int>(type: "int", nullable: true),
                    volume = table.Column<double>(type: "float", nullable: true),
                    insertdate = table.Column<DateTime>(name: "insert_date", type: "datetime", nullable: true),
                    tag = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeaTransportDetail", x => x.id);
                    table.ForeignKey(
                        name: "FK_SeaTransportDetail_SeaTransport",
                        column: x => x.seatransportid,
                        principalTable: "SeaTransport",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AirTransport_customer_id",
                table: "AirTransport",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_AirTransportDetail_air_transport_id",
                table: "AirTransportDetail",
                column: "air_transport_id");

            migrationBuilder.CreateIndex(
                name: "IX_SeaTransport_customer_id",
                table: "SeaTransport",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_SeaTransportDetail_sea_transport_id",
                table: "SeaTransportDetail",
                column: "sea_transport_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirTransportDetail");

            migrationBuilder.DropTable(
                name: "ExpressTransport");

            migrationBuilder.DropTable(
                name: "ExpressTransportDetail");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "SeaTransportDetail");

            migrationBuilder.DropTable(
                name: "AirTransport");

            migrationBuilder.DropTable(
                name: "SeaTransport");

            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
