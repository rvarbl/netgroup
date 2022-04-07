using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    public partial class Migration220407_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttributes_Attributes_ItemAttributeId",
                table: "ItemAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemAttributes_StorageItems_StorageItemId",
                table: "ItemAttributes");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Attributes_ItemAttributeId",
                table: "Storages");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_ItemAttributes_ItemAttributeId",
                table: "ItemAttributes");

            migrationBuilder.DropIndex(
                name: "IX_ItemAttributes_StorageItemId",
                table: "ItemAttributes");

            migrationBuilder.DropColumn(
                name: "AttributeValue",
                table: "ItemAttributes");

            migrationBuilder.DropColumn(
                name: "ItemAttributeId",
                table: "ItemAttributes");

            migrationBuilder.DropColumn(
                name: "StorageItemId",
                table: "ItemAttributes");

            migrationBuilder.AddColumn<string>(
                name: "AttributeName",
                table: "ItemAttributes",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AttributeInItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemAttributeId = table.Column<Guid>(type: "uuid", nullable: false),
                    StorageItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeValue = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Comment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeInItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeInItems_ItemAttributes_ItemAttributeId",
                        column: x => x.ItemAttributeId,
                        principalTable: "ItemAttributes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttributeInItems_StorageItems_StorageItemId",
                        column: x => x.StorageItemId,
                        principalTable: "StorageItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeInItems_ItemAttributeId",
                table: "AttributeInItems",
                column: "ItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeInItems_StorageItemId",
                table: "AttributeInItems",
                column: "StorageItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_ItemAttributes_ItemAttributeId",
                table: "Storages",
                column: "ItemAttributeId",
                principalTable: "ItemAttributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_ItemAttributes_ItemAttributeId",
                table: "Storages");

            migrationBuilder.DropTable(
                name: "AttributeInItems");

            migrationBuilder.DropColumn(
                name: "AttributeName",
                table: "ItemAttributes");

            migrationBuilder.AddColumn<string>(
                name: "AttributeValue",
                table: "ItemAttributes",
                type: "character varying(512)",
                maxLength: 512,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ItemAttributeId",
                table: "ItemAttributes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "StorageItemId",
                table: "ItemAttributes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AttributeName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Comment = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributes_ItemAttributeId",
                table: "ItemAttributes",
                column: "ItemAttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemAttributes_StorageItemId",
                table: "ItemAttributes",
                column: "StorageItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttributes_Attributes_ItemAttributeId",
                table: "ItemAttributes",
                column: "ItemAttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemAttributes_StorageItems_StorageItemId",
                table: "ItemAttributes",
                column: "StorageItemId",
                principalTable: "StorageItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Attributes_ItemAttributeId",
                table: "Storages",
                column: "ItemAttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
