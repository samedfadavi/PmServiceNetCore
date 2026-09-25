using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PmServiceNetCode.Migrations
{
    /// <inheritdoc />
    public partial class addauthorization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Catout",
                columns: table => new
                {
                    Code_Catout = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Catout = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code_Parent = table.Column<int>(type: "int", nullable: true),
                    CO = table.Column<int>(type: "int", nullable: true),
                    ANE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ANK = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Jaryan = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Noe = table.Column<int>(type: "int", nullable: true),
                    X = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Y = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hozeh = table.Column<int>(type: "int", nullable: true),
                    Sazande = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sal_Sakht = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Catout", x => x.Code_Catout);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Derakht_Tajhizat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    Bazdid = table.Column<int>(type: "int", nullable: true),
                    Parent_tajhiz = table.Column<int>(type: "int", nullable: true),
                    Sharh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code_No_DerakhtTajhizat = table.Column<int>(type: "int", nullable: true),
                    Name_Layeh_Gis = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name_Jadval_Pm = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code_Pm = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name_Pm = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Field_Rabet = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    image = table.Column<int>(type: "int", nullable: true),
                    Goroh_Tajhiz = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Derakht_Tajhizat", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Farayand",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Onvan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grouh = table.Column<int>(type: "int", nullable: true),
                    NameSabtKonande = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TarikhSabt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Farayand", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_FFM",
                columns: table => new
                {
                    Code_FFM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bakhsh = table.Column<int>(type: "int", nullable: true),
                    PFT = table.Column<int>(type: "int", nullable: true),
                    Date_FFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TFider_FFM = table.Column<int>(type: "int", nullable: true),
                    Havaee_FFM = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    Zamini_FFM = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    ANami_FFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MFider_FFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BarS_FFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Diagram_FFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NFider_FFM = table.Column<byte>(type: "tinyint", nullable: true),
                    Ghati_FFM = table.Column<decimal>(type: "decimal(8,4)", precision: 8, scale: 4, nullable: true),
                    X_FFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Y_FFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    noemasraf_ffm = table.Column<int>(type: "int", nullable: true),
                    zamanbandi = table.Column<int>(type: "int", nullable: true),
                    hozeh = table.Column<int>(type: "int", nullable: true),
                    MAPCODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SoholateDastresi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DomadareBa = table.Column<int>(type: "int", nullable: true),
                    DomadareBa2 = table.Column<int>(type: "int", nullable: true),
                    MetrajDomadareBa1 = table.Column<int>(type: "int", nullable: true),
                    MetrajDomadareBa2 = table.Column<int>(type: "int", nullable: true),
                    FiderHasas = table.Column<bool>(type: "bit", nullable: true),
                    Name_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Shomareh_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BarS_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SPBar_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KFider_Fider = table.Column<byte>(type: "tinyint", nullable: true),
                    TZsef_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TZef_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TZoc_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    THsef_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    THef_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    THoc_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NTsef_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NTef_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NToc_Fider = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code_121 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_FFM", x => x.Code_FFM);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Forms",
                columns: table => new
                {
                    ID_Form = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Onvan = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Forms", x => x.ID_Form);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Omoor",
                columns: table => new
                {
                    Code_Omoor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id = table.Column<int>(type: "int", nullable: true),
                    Ostan = table.Column<int>(type: "int", nullable: true),
                    Shahr = table.Column<int>(type: "int", nullable: true),
                    Name_Omoor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Daraje_Omoor = table.Column<int>(type: "int", nullable: true),
                    Id_Gis = table.Column<int>(type: "int", nullable: true),
                    code_moshtarekin = table.Column<int>(type: "int", nullable: true),
                    code_121 = table.Column<int>(type: "int", nullable: true),
                    lvr = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Omoor", x => x.Code_Omoor);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PayehFFM",
                columns: table => new
                {
                    Code_Payeh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code_FFM = table.Column<int>(type: "int", nullable: true),
                    NamePayeh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Address_Payeh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tozihat_Payeh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    X_Payeh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Y_Payeh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Noe_Payeh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Keshesh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ertefa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hozeh = table.Column<int>(type: "int", nullable: true),
                    MAPCODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sal_sakht = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    noe_arayesh = table.Column<short>(type: "smallint", nullable: true),
                    sazande = table.Column<short>(type: "smallint", nullable: true),
                    vaz_paye = table.Column<short>(type: "smallint", nullable: true),
                    earth = table.Column<short>(type: "smallint", nullable: true),
                    mgh_erth = table.Column<int>(type: "int", nullable: true),
                    noe_fondasoun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    jelobar = table.Column<short>(type: "smallint", nullable: true),
                    num_madar_payeh = table.Column<int>(type: "int", nullable: true),
                    sal_nasb = table.Column<int>(type: "int", nullable: true),
                    tedad_mghr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    noe_kar = table.Column<short>(type: "smallint", nullable: true),
                    GlobalID_Old = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    emtiaz = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetOmomi = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetKhamoshi = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetBargiri = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetDoreBazdid = table.Column<double>(type: "float", nullable: true),
                    GisObjectId = table.Column<long>(type: "bigint", nullable: true),
                    Code_Payeh_121 = table.Column<int>(type: "int", nullable: true),
                    tole_shabake_moshajjar = table.Column<double>(type: "float", nullable: true),
                    GlobalID_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    noe_cheragh = table.Column<int>(type: "int", nullable: true),
                    tavan = table.Column<int>(type: "int", nullable: true),
                    tole_shabake_dargir = table.Column<double>(type: "float", nullable: true),
                    Code_Section_Gis = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Code_Section_121 = table.Column<long>(type: "bigint", nullable: true),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hazf = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PayehFFM", x => x.Code_Payeh);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PayehFFZ",
                columns: table => new
                {
                    Code_Payeh = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code_FFZ = table.Column<int>(type: "int", nullable: true),
                    NamePayeh = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Address_Payeh = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Tozihat_Payeh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    X_Payeh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Y_Payeh = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Noe_Payeh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Keshesh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Ertefa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hozeh = table.Column<int>(type: "int", nullable: true),
                    MAPCODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sal_sakht = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    sal_nasb = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Sazande = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    vaz_paye = table.Column<short>(type: "smallint", nullable: true),
                    Earth = table.Column<short>(type: "smallint", nullable: true),
                    mgh_erth = table.Column<int>(type: "int", nullable: true),
                    noe_fondasoun = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Jelobar = table.Column<short>(type: "smallint", nullable: true),
                    noe_arayesh_paye = table.Column<short>(type: "smallint", nullable: true),
                    noe_kar = table.Column<short>(type: "smallint", nullable: true),
                    GlobalID_Old = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    noe_cheragh = table.Column<int>(type: "int", nullable: true),
                    tavan = table.Column<int>(type: "int", nullable: true),
                    tole_shabake_moshajjar = table.Column<double>(type: "float", nullable: true),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hazf = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PayehFFZ", x => x.Code_Payeh);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PFT",
                columns: table => new
                {
                    Code_PFT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_PFT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Omoor = table.Column<int>(type: "int", nullable: true),
                    Ghodrat_PFT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Date_PFT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_PFT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tozihat_PFT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    X_PFT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Y_PFT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Map_cod_ol = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PFT", x => x.Code_PFT);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PT",
                columns: table => new
                {
                    Code_PT = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_PT = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    QFFM = table.Column<int>(type: "int", nullable: true),
                    Masir20 = table.Column<int>(type: "int", nullable: true),
                    Ghodrat_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoeP_PT = table.Column<int>(type: "int", nullable: true),
                    Karbari_PT = table.Column<short>(type: "smallint", nullable: true),
                    Karbari_PT_old = table.Column<short>(type: "smallint", nullable: true),
                    Date_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address_PT = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    X_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Y_PT = table.Column<double>(type: "float", nullable: true),
                    SakhtemanPost_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoeSystemZamin_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Moqavemqt_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Boof_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RTU_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoeTahviye_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoeMasraf_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MoshakhaseKelid_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VaziyatErtebat_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChaheNol_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ChaheBarq_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    KhateGarm_PT = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Shserial_Pt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    emtiaz = table.Column<double>(type: "float", nullable: true),
                    zamanbandi = table.Column<int>(type: "int", nullable: true),
                    hozeh = table.Column<int>(type: "int", nullable: true),
                    MAPCODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pelak = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    SoholateDastresi = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ghodrat_PT2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ghodrat_PT3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ghodrat_PT4 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZamanRaftoBargasht = table.Column<int>(type: "int", nullable: true),
                    EmtiazSharayetBargiri = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetKhamoshi = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetOmomi = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetDoreBazdid = table.Column<double>(type: "float", nullable: true),
                    GlobalID_Old = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ZamanVoghPik = table.Column<int>(type: "int", nullable: true),
                    GisObjectId = table.Column<long>(type: "bigint", nullable: true),
                    Code_Pt_121 = table.Column<int>(type: "int", nullable: true),
                    GlobalID_1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID_2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID_3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Olaviat_Kharabi = table.Column<double>(type: "float", nullable: true),
                    SalSakht = table.Column<int>(type: "int", nullable: true),
                    count_tfz1 = table.Column<double>(type: "float", nullable: true),
                    Pelak_121_New = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RCA_JaryanNami = table.Column<double>(type: "float", nullable: true),
                    RCA_Barghgir = table.Column<bool>(type: "bit", nullable: true),
                    RCA_JaryanKelid = table.Column<double>(type: "float", nullable: true),
                    RCA_VaziatSelikajol = table.Column<bool>(type: "bit", nullable: true),
                    RCA_Andaze_Ert = table.Column<double>(type: "float", nullable: true),
                    RCA_TanzimKelid = table.Column<double>(type: "float", nullable: true),
                    RCA_KablBaghgir = table.Column<bool>(type: "bit", nullable: true),
                    RCA_BarPik = table.Column<double>(type: "float", nullable: true),
                    RCA_BarBahrebardari = table.Column<double>(type: "float", nullable: true),
                    RCA_KeshvarSazande = table.Column<double>(type: "float", nullable: true),
                    count_tfz2 = table.Column<double>(type: "float", nullable: true),
                    Code_Section_Gis = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Code_Section_121 = table.Column<long>(type: "bigint", nullable: true),
                    T_ID_CODE = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Add_Bargiri = table.Column<double>(type: "float", nullable: true),
                    GlobalID = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    hazf = table.Column<int>(type: "int", nullable: true),
                    ezafe = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PT", x => x.Code_PT);
                });

            migrationBuilder.CreateTable(
                name: "tbl_QFFM",
                columns: table => new
                {
                    Code_QFFM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fider = table.Column<int>(type: "int", nullable: true),
                    Bakhsh = table.Column<int>(type: "int", nullable: true),
                    NMasir_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AddressM_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Moshajjar_QFFM = table.Column<int>(type: "int", nullable: true),
                    JMasir_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EbMasir_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnMasir_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NShabakeh_QFFM = table.Column<byte>(type: "tinyint", nullable: true),
                    TKMasir_QFFM = table.Column<int>(type: "int", nullable: true),
                    Havaee_QFFM = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    Zamini_QFFM = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: true),
                    Xsh_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ysh_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Xp_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Yp_QFFM = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NoeHadi = table.Column<int>(type: "int", nullable: true),
                    Maqta = table.Column<int>(type: "int", nullable: true),
                    hozeh = table.Column<int>(type: "int", nullable: true),
                    emtiaz = table.Column<double>(type: "float", nullable: true),
                    ZamanRaftoBargasht = table.Column<int>(type: "int", nullable: true),
                    EmtiazSharayetOmomi = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetKhamoshi = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetBargiri = table.Column<double>(type: "float", nullable: true),
                    EmtiazSharayetDoreBazdid = table.Column<double>(type: "float", nullable: true),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_QFFM", x => x.Code_QFFM);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Secsuner",
                columns: table => new
                {
                    Code_Secsuner = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Secsuner = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Code_Parent = table.Column<int>(type: "int", nullable: true),
                    PT = table.Column<int>(type: "int", nullable: true),
                    CO = table.Column<int>(type: "int", nullable: true),
                    NoeSecsuner = table.Column<int>(type: "int", nullable: true),
                    Serial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GlobalID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hozeh = table.Column<int>(type: "int", nullable: true),
                    Sazande = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sal_Sakht = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Secsuner", x => x.Code_Secsuner);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Tablo",
                columns: table => new
                {
                    Code_Tablo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Tablo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code_Parent = table.Column<int>(type: "int", nullable: true),
                    PT = table.Column<int>(type: "int", nullable: true),
                    CO = table.Column<int>(type: "int", nullable: true),
                    GKelid_Tablo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CT_Tablo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Connect_Tablo = table.Column<byte>(type: "tinyint", nullable: true),
                    Date_Tablo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TedadTablo_Tablo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TedadCell_Tablo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Maqta_Tablo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ertelektriki = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Erthefazati = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    globalid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sazande = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    sale_sakht = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    sale_nasb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    hozeh = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tablo", x => x.Code_Tablo);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Trance",
                columns: table => new
                {
                    Code_Trance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_Trance = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Serial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code_Parent = table.Column<int>(type: "int", nullable: true),
                    globalid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    sazande = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    sale_sakht = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    sale_nasb = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    zarfiat = table.Column<int>(type: "int", nullable: true),
                    hozeh = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Trance", x => x.Code_Trance);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    Code_User = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password_User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessLevel = table.Column<int>(type: "int", nullable: true),
                    no_karbar = table.Column<int>(type: "int", nullable: true),
                    code_omor = table.Column<int>(type: "int", nullable: true),
                    code_bakhsh = table.Column<int>(type: "int", nullable: true),
                    Flag_N = table.Column<int>(type: "int", nullable: true),
                    code_Role = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User", x => x.Code_User);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Code_User",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "tbl_Catout");

            migrationBuilder.DropTable(
                name: "Tbl_Derakht_Tajhizat");

            migrationBuilder.DropTable(
                name: "Tbl_Farayand");

            migrationBuilder.DropTable(
                name: "tbl_FFM");

            migrationBuilder.DropTable(
                name: "Tbl_Forms");

            migrationBuilder.DropTable(
                name: "tbl_Omoor");

            migrationBuilder.DropTable(
                name: "tbl_PayehFFM");

            migrationBuilder.DropTable(
                name: "tbl_PayehFFZ");

            migrationBuilder.DropTable(
                name: "tbl_PFT");

            migrationBuilder.DropTable(
                name: "tbl_PT");

            migrationBuilder.DropTable(
                name: "tbl_QFFM");

            migrationBuilder.DropTable(
                name: "tbl_Secsuner");

            migrationBuilder.DropTable(
                name: "tbl_Tablo");

            migrationBuilder.DropTable(
                name: "tbl_Trance");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "tbl_User");
        }
    }
}
