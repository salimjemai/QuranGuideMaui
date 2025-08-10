using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace QuranGuide.Maui.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ayahs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    NumberInSurah = table.Column<int>(type: "integer", nullable: false),
                    Juz = table.Column<int>(type: "integer", nullable: false),
                    Manzil = table.Column<int>(type: "integer", nullable: false),
                    Page = table.Column<int>(type: "integer", nullable: false),
                    Ruku = table.Column<int>(type: "integer", nullable: false),
                    HizbQuarter = table.Column<int>(type: "integer", nullable: false),
                    SajdaType = table.Column<string>(type: "text", nullable: false),
                    SajdaNumber = table.Column<int>(type: "integer", nullable: false),
                    SurahId = table.Column<int>(type: "integer", nullable: false),
                    EditionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ayahs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DownloadedContent",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    LocalPath = table.Column<string>(type: "text", nullable: false),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadedContent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Identifier = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EnglishName = table.Column<string>(type: "text", nullable: false),
                    Format = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Direction = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hadiths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HadithNumber = table.Column<string>(type: "text", nullable: false),
                    EnglishNarrator = table.Column<string>(type: "text", nullable: false),
                    HadithEnglish = table.Column<string>(type: "text", nullable: false),
                    HadithArabic = table.Column<string>(type: "text", nullable: false),
                    HeadingEnglish = table.Column<string>(type: "text", nullable: false),
                    HeadingArabic = table.Column<string>(type: "text", nullable: false),
                    ChapterId = table.Column<string>(type: "text", nullable: false),
                    BookSlug = table.Column<string>(type: "text", nullable: false),
                    Volume = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hadiths", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Surahs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EnglishName = table.Column<string>(type: "text", nullable: false),
                    EnglishNameTranslation = table.Column<string>(type: "text", nullable: false),
                    RevelationType = table.Column<string>(type: "text", nullable: false),
                    NumberOfAyahs = table.Column<int>(type: "integer", nullable: false),
                    Juz = table.Column<int>(type: "integer", nullable: false),
                    HizbQuarter = table.Column<int>(type: "integer", nullable: false),
                    SajdaType = table.Column<string>(type: "text", nullable: false),
                    SajdaNumber = table.Column<int>(type: "integer", nullable: false),
                    Ruku = table.Column<int>(type: "integer", nullable: false),
                    Page = table.Column<int>(type: "integer", nullable: false),
                    Manzil = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surahs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPreferences",
                columns: table => new
                {
                    SelectedEdition = table.Column<string>(type: "text", nullable: false),
                    SelectedLanguage = table.Column<string>(type: "text", nullable: false),
                    SelectedType = table.Column<string>(type: "text", nullable: false),
                    SelectedFormat = table.Column<string>(type: "text", nullable: false),
                    DarkMode = table.Column<bool>(type: "boolean", nullable: false),
                    FontSize = table.Column<int>(type: "integer", nullable: false),
                    AutoPlayAudio = table.Column<bool>(type: "boolean", nullable: false),
                    OfflineMode = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ayahs_SurahId_NumberInSurah_EditionId",
                table: "Ayahs",
                columns: new[] { "SurahId", "NumberInSurah", "EditionId" });

            migrationBuilder.CreateIndex(
                name: "IX_Surahs_Number",
                table: "Surahs",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ayahs");

            migrationBuilder.DropTable(
                name: "DownloadedContent");

            migrationBuilder.DropTable(
                name: "Editions");

            migrationBuilder.DropTable(
                name: "Hadiths");

            migrationBuilder.DropTable(
                name: "Surahs");

            migrationBuilder.DropTable(
                name: "UserPreferences");
        }
    }
}
