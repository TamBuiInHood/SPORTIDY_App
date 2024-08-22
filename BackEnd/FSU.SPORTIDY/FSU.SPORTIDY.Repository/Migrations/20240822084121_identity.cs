using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FSU.SPORTIDY.Repository.Migrations
{
    /// <inheritdoc />
    public partial class identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Club",
                columns: table => new
                {
                    ClubId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClubCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    ClubName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Regulation = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Infomation = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Slogan = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    MainSport = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Location = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    TotalMember = table.Column<int>(type: "int", nullable: true),
                    AvartarClub = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    CoverImageClub = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Club__D35058E79F8F62DE", x => x.ClubId);
                });

            migrationBuilder.CreateTable(
                name: "Meeting",
                columns: table => new
                {
                    MeetingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MeetingCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    MeetingName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MeetingImage = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    Address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Host = table.Column<int>(type: "int", nullable: true),
                    TotalMember = table.Column<int>(type: "int", nullable: true),
                    ClubID = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsPublic = table.Column<bool>(type: "bit", nullable: true),
                    SportID = table.Column<int>(type: "int", nullable: true),
                    CancelBefore = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Meeting__E9F9E9AC468584B2", x => x.MeetingID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__8AFACE3A038ACFA2", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Sport",
                columns: table => new
                {
                    SportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SportCode = table.Column<int>(type: "int", nullable: true),
                    SportName = table.Column<int>(type: "int", nullable: true),
                    SportIamge = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sport__7A41AF1C1AA63361", x => x.SportID);
                });

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    UserTokenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpAccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserToken__C423424FG41", x => x.UserTokenId);
                });

            migrationBuilder.CreateTable(
                name: "CommentInMeeting",
                columns: table => new
                {
                    CommentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    CommentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MeetingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CommentI__C3B4DFAA471104AD", x => x.CommentID);
                    table.ForeignKey(
                        name: "FK__CommentIn__Meeti__534D60F1",
                        column: x => x.MeetingID,
                        principalTable: "Meeting",
                        principalColumn: "MeetingID");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    FullName = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    OTP = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Avartar = table.Column<string>(type: "varchar(2000)", unicode: false, maxLength: 2000, nullable: true),
                    Birtday = table.Column<DateOnly>(type: "date", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__User__1788CCACB5F6D77C", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__User__RoleID__59FA5E80",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "RoleID");
                });

            migrationBuilder.CreateTable(
                name: "Friendship",
                columns: table => new
                {
                    FriendShipID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendShipCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateOnly>(type: "date", nullable: true),
                    UserID1 = table.Column<int>(type: "int", nullable: false),
                    UserID2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Friendsh__190D637884858AB7", x => x.FriendShipID);
                    table.ForeignKey(
                        name: "FK__Friendshi__UserI__5441852A",
                        column: x => x.UserID1,
                        principalTable: "User",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__Friendshi__UserI__5535A963",
                        column: x => x.UserID2,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    Tiltle = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    Message = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    NotificationType = table.Column<bool>(type: "bit", nullable: true),
                    IsAccept = table.Column<bool>(type: "bit", nullable: true),
                    InviteDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Notifica__20CF2E32B70E75BB", x => x.NotificationID);
                    table.ForeignKey(
                        name: "FK__Notificat__UserI__571DF1D5",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "PlayField",
                columns: table => new
                {
                    PlayFieldID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayFieldCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    PlayFieldName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    OpenTime = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    CloseTime = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PlayFiel__4E6EFC93249A0D85", x => x.PlayFieldID);
                    table.ForeignKey(
                        name: "FK_PlayField_User",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserClub",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ClubId = table.Column<int>(type: "int", nullable: false),
                    IsLeader = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserClub__7ABDC9227D05295A", x => new { x.UserID, x.ClubId });
                    table.ForeignKey(
                        name: "FK__UserClub__ClubId__5AEE82B9",
                        column: x => x.ClubId,
                        principalTable: "Club",
                        principalColumn: "ClubId");
                    table.ForeignKey(
                        name: "FK__UserClub__UserID__5BE2A6F2",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserMeeting",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MeetingID = table.Column<int>(type: "int", nullable: false),
                    ClubID = table.Column<int>(type: "int", nullable: true),
                    RoleInMeeting = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserMeet__C9175236CBB90D5E", x => new { x.UserID, x.MeetingID });
                    table.ForeignKey(
                        name: "FK__UserMeeti__Meeti__5CD6CB2B",
                        column: x => x.MeetingID,
                        principalTable: "Meeting",
                        principalColumn: "MeetingID");
                    table.ForeignKey(
                        name: "FK__UserMeeti__UserI__5DCAEF64",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "UserSport",
                columns: table => new
                {
                    SportID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__UserSpor__AB3923D6A026E1F3", x => new { x.SportID, x.UserID });
                    table.ForeignKey(
                        name: "FK__UserSport__Sport__5EBF139D",
                        column: x => x.SportID,
                        principalTable: "Sport",
                        principalColumn: "SportID");
                    table.ForeignKey(
                        name: "FK__UserSport__UserI__5FB337D6",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingCode = table.Column<int>(type: "int", nullable: true),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateEnd = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: true),
                    BarCode = table.Column<int>(type: "int", nullable: true),
                    PlayFieldID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Booking__73951ACD27B15586", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK__Booking__PlayFie__5070F446",
                        column: x => x.PlayFieldID,
                        principalTable: "PlayField",
                        principalColumn: "PlayFieldID");
                });

            migrationBuilder.CreateTable(
                name: "ImageField",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageURL = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    VideoURL = table.Column<int>(type: "int", nullable: true),
                    IsSportlight = table.Column<bool>(type: "bit", nullable: true),
                    PlayFieldID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ImageFie__7516F4EC9A3F7261", x => x.ImageID);
                    table.ForeignKey(
                        name: "FK__ImageFiel__PlayF__4316F928",
                        column: x => x.PlayFieldID,
                        principalTable: "PlayField",
                        principalColumn: "PlayFieldID");
                });

            migrationBuilder.CreateTable(
                name: "PlayFieldFeedback",
                columns: table => new
                {
                    FeedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeedbackCode = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: true),
                    Content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: true),
                    FeedbackDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ImageURL = table.Column<int>(type: "int", nullable: true),
                    VideoURL = table.Column<int>(type: "int", nullable: true),
                    IsAnonymous_ = table.Column<bool>(type: "bit", nullable: true),
                    BookingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PlayFiel__6A4BEDF64C6D3772", x => x.FeedbackID);
                    table.ForeignKey(
                        name: "FK__PlayField__Booki__59063A47",
                        column: x => x.BookingID,
                        principalTable: "Booking",
                        principalColumn: "BookingID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PlayFieldID",
                table: "Booking",
                column: "PlayFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_CommentInMeeting_MeetingID",
                table: "CommentInMeeting",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_UserID1",
                table: "Friendship",
                column: "UserID1");

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_UserID2",
                table: "Friendship",
                column: "UserID2");

            migrationBuilder.CreateIndex(
                name: "IX_ImageField_PlayFieldID",
                table: "ImageField",
                column: "PlayFieldID");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserID",
                table: "Notification",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayField_UserID",
                table: "PlayField",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PlayFieldFeedback_BookingID",
                table: "PlayFieldFeedback",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleID",
                table: "User",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserClub_ClubId",
                table: "UserClub",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMeeting_MeetingID",
                table: "UserMeeting",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_UserSport_UserID",
                table: "UserSport",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentInMeeting");

            migrationBuilder.DropTable(
                name: "Friendship");

            migrationBuilder.DropTable(
                name: "ImageField");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "PlayFieldFeedback");

            migrationBuilder.DropTable(
                name: "UserClub");

            migrationBuilder.DropTable(
                name: "UserMeeting");

            migrationBuilder.DropTable(
                name: "UserSport");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Club");

            migrationBuilder.DropTable(
                name: "Meeting");

            migrationBuilder.DropTable(
                name: "Sport");

            migrationBuilder.DropTable(
                name: "PlayField");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
