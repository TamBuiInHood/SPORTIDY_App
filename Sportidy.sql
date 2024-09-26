USE [master]
GO
DROP DATABASE [Sportidy]
GO
CREATE DATABASE [Sportidy]
GO
USE [Sportidy]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingID] [int] IDENTITY(1,1) NOT NULL,
	[BookingCode] [nvarchar](max) NULL,
	[BookingDate] [datetime] NULL,
	[Price] [float] NULL,
	[DateStart] [datetime] NULL,
	[DateEnd] [datetime] NULL,
	[Status] [int] NULL,
	[PaymentMethod] [nvarchar](max) NULL,
	[BarCode] [nvarchar](max) NULL,
	[PlayFieldID] [int] NOT NULL,
	[Description] [text] NULL,
	[CustomerID] [int] NULL,
 CONSTRAINT [PK__Booking__73951ACD27B15586] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Club]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Club](
	[ClubId] [int] IDENTITY(1,1) NOT NULL,
	[ClubCode] [varchar](36) NULL,
	[ClubName] [varchar](max) NULL,
	[Regulation] [varchar](max) NULL,
	[Infomation] [varchar](max) NULL,
	[Slogan] [varchar](200) NULL,
	[MainSport] [varchar](100) NULL,
	[CreateDate] [date] NULL,
	[Location] [varchar](200) NULL,
	[TotalMember] [int] NULL,
	[AvartarClub] [varchar](max) NULL,
	[CoverImageClub] [varchar](max) NULL,
 CONSTRAINT [PK__Club__D35058E79F8F62DE] PRIMARY KEY CLUSTERED 
(
	[ClubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommentInMeeting]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentInMeeting](
	[CommentID] [int] IDENTITY(1,1) NOT NULL,
	[CommentCode] [varchar](36) NULL,
	[CommentDate] [datetime] NULL,
	[UserID] [int] NOT NULL,
	[Content] [varchar](max) NULL,
	[Image] [varchar](max) NULL,
	[MeetingID] [int] NOT NULL,
 CONSTRAINT [PK__CommentI__C3B4DFAA471104AD] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Friendship]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friendship](
	[FriendShipID] [int] IDENTITY(1,1) NOT NULL,
	[FriendShipCode] [varchar](36) NULL,
	[Status] [int] NULL,
	[CreateDate] [date] NULL,
	[UserID1] [int] NOT NULL,
	[UserID2] [int] NOT NULL,
	[RequestBy] [int] NOT NULL,
 CONSTRAINT [PK__Friendsh__190D637884858AB7] PRIMARY KEY CLUSTERED 
(
	[FriendShipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageField]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageField](
	[ImageID] [int] IDENTITY(1,1) NOT NULL,
	[ImageURL] [varchar](300) NULL,
	[VideoURL] [nvarchar](max) NULL,
	[ImageIndex] [int] NULL,
	[PlayFieldID] [int] NULL,
 CONSTRAINT [PK__ImageFie__7516F4EC9A3F7261] PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meeting]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meeting](
	[MeetingID] [int] IDENTITY(1,1) NOT NULL,
	[MeetingCode] [varchar](36) NULL,
	[MeetingName] [varchar](100) NULL,
	[MeetingImage] [varchar](300) NULL,
	[Address] [varchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Host] [int] NULL,
	[TotalMember] [int] NULL,
	[ClubID] [int] NULL,
	[Note] [varchar](max) NULL,
	[IsPublic] [bit] NULL,
	[SportID] [int] NULL,
	[CancelBefore] [int] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK__Meeting__E9F9E9AC468584B2] PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL,
	[NotificationCode] [varchar](240) NULL,
	[Tiltle] [varchar](max) NULL,
	[Message] [varchar](max) NULL,
	[NotificationType] [bit] NULL,
	[IsAccept] [bit] NULL,
	[InviteDate] [datetime] NULL,
	[IsRead] [bit] NULL,
	[UserID] [int] NULL,
 CONSTRAINT [PK__Notifica__20CF2E32B70E75BB] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[OrderCode] [nvarchar](max) NULL,
	[Amount] [float] NULL,
	[DateOfTransaction] [datetime] NULL,
	[Status] [int] NULL,
	[BookingId] [int] NULL,
 CONSTRAINT [PK__Payment__7A41AF1C1AA63361] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayField]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayField](
	[PlayFieldID] [int] IDENTITY(1,1) NOT NULL,
	[PlayFieldCode] [varchar](36) NULL,
	[PlayFieldName] [varchar](200) NULL,
	[Price] [float] NULL,
	[Address] [varchar](200) NULL,
	[OpenTime] [time](7) NULL,
	[UserID] [int] NULL,
	[IsDependency] [int] NULL,
	[CloseTime] [time](7) NULL,
	[AvatarImage] [nvarchar](max) NULL,
	[SportId] [int] NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK__PlayFiel__4E6EFC93249A0D85] PRIMARY KEY CLUSTERED 
(
	[PlayFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayFieldFeedback]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayFieldFeedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackCode] [varchar](36) NULL,
	[Content] [nvarchar](max) NULL,
	[Rating] [int] NULL,
	[FeedbackDate] [datetime] NULL,
	[ImageURL] [nvarchar](max) NULL,
	[VideoURL] [nvarchar](max) NULL,
	[IsAnonymous_] [bit] NULL,
	[BookingID] [int] NOT NULL,
 CONSTRAINT [PK__PlayFiel__6A4BEDF64C6D3772] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK__Role__8AFACE3A038ACFA2] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sport]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sport](
	[SportID] [int] IDENTITY(1,1) NOT NULL,
	[SportCode] [nvarchar](max) NULL,
	[SportName] [nvarchar](max) NULL,
	[SportImage] [nvarchar](max) NULL,
 CONSTRAINT [PK__Sport__7A41AF1C1AA63361] PRIMARY KEY CLUSTERED 
(
	[SportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemFeedback]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemFeedback](
	[FeedbackID] [int] IDENTITY(1,1) NOT NULL,
	[FeedbackCode] [varchar](36) NULL,
	[Content] [nvarchar](max) NULL,
	[Rating] [int] NULL,
	[FeedbackDate] [datetime] NULL,
	[ImageURL] [nvarchar](max) NULL,
	[VideoURL] [nvarchar](max) NULL,
	[IsAnonymous_] [bit] NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK__SystemFeedback__6A4BEDF64C6D3772] PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserCode] [varchar](36) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](200) NULL,
	[FullName] [varchar](80) NULL,
	[Address] [varchar](200) NULL,
	[OTP] [varchar](20) NULL,
	[Email] [varchar](200) NULL,
	[Gender] [int] NULL,
	[Description] [varchar](200) NULL,
	[Avartar] [varchar](2000) NULL,
	[Birtday] [date] NULL,
	[RoleID] [int] NOT NULL,
	[CreateDate] [date] NULL,
	[Status] [int] NULL,
	[IsDeleted] [int] NULL,
	[Phone] [nvarchar](20) NULL,
	[BankCode] [nvarchar](max) NULL,
	[BankName] [nvarchar](max) NULL,
	[DeviceCode] [nvarchar](max) NULL,
 CONSTRAINT [PK__User__1788CCACB5F6D77C] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClub]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClub](
	[UserID] [int] NOT NULL,
	[ClubId] [int] NOT NULL,
	[IsLeader] [bit] NULL,
 CONSTRAINT [PK__UserClub__7ABDC9227D05295A] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ClubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMeeting]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMeeting](
	[UserID] [int] NOT NULL,
	[MeetingID] [int] NOT NULL,
	[ClubID] [int] NULL,
	[RoleInMeeting] [nvarchar](50) NULL,
 CONSTRAINT [PK__UserMeet__C9175236CBB90D5E] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSport]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSport](
	[SportID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK__UserSpor__AB3923D6A026E1F3] PRIMARY KEY CLUSTERED 
(
	[SportID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserToken]    Script Date: 9/26/2024 10:39:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserToken](
	[UserTokenId] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[AccessToken] [nvarchar](max) NOT NULL,
	[ExpAccessToken] [nvarchar](max) NOT NULL,
	[RefreshToken] [nvarchar](max) NOT NULL,
	[ExpRefreshToken] [nvarchar](max) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK__UserToken__C423424FG41] PRIMARY KEY CLUSTERED 
(
	[UserTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Friendship] ADD  DEFAULT ((0)) FOR [RequestBy]
GO
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__PlayFie__5070F446] FOREIGN KEY([PlayFieldID])
REFERENCES [dbo].[PlayField] ([PlayFieldID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__PlayFie__5070F446]
GO
ALTER TABLE [dbo].[CommentInMeeting]  WITH CHECK ADD  CONSTRAINT [FK__CommentIn__Meeti__534D60F1] FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meeting] ([MeetingID])
GO
ALTER TABLE [dbo].[CommentInMeeting] CHECK CONSTRAINT [FK__CommentIn__Meeti__534D60F1]
GO
ALTER TABLE [dbo].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK__Friendshi__UserI__5441852A] FOREIGN KEY([UserID1])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Friendship] CHECK CONSTRAINT [FK__Friendshi__UserI__5441852A]
GO
ALTER TABLE [dbo].[Friendship]  WITH CHECK ADD  CONSTRAINT [FK__Friendshi__UserI__5535A963] FOREIGN KEY([UserID2])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Friendship] CHECK CONSTRAINT [FK__Friendshi__UserI__5535A963]
GO
ALTER TABLE [dbo].[ImageField]  WITH CHECK ADD  CONSTRAINT [FK__ImageFiel__PlayF__4316F928] FOREIGN KEY([PlayFieldID])
REFERENCES [dbo].[PlayField] ([PlayFieldID])
GO
ALTER TABLE [dbo].[ImageField] CHECK CONSTRAINT [FK__ImageFiel__PlayF__4316F928]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK__Notificat__UserI__571DF1D5] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK__Notificat__UserI__571DF1D5]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK__Payment__Booking__59063A47] FOREIGN KEY([BookingId])
REFERENCES [dbo].[Booking] ([BookingID])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK__Payment__Booking__59063A47]
GO
ALTER TABLE [dbo].[PlayField]  WITH CHECK ADD  CONSTRAINT [FK_PlayField_PlayField] FOREIGN KEY([IsDependency])
REFERENCES [dbo].[PlayField] ([PlayFieldID])
GO
ALTER TABLE [dbo].[PlayField] CHECK CONSTRAINT [FK_PlayField_PlayField]
GO
ALTER TABLE [dbo].[PlayField]  WITH CHECK ADD  CONSTRAINT [FK_PlayField_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PlayField] CHECK CONSTRAINT [FK_PlayField_User]
GO
ALTER TABLE [dbo].[PlayFieldFeedback]  WITH CHECK ADD  CONSTRAINT [FK__PlayField__Booki__59063A47] FOREIGN KEY([BookingID])
REFERENCES [dbo].[Booking] ([BookingID])
GO
ALTER TABLE [dbo].[PlayFieldFeedback] CHECK CONSTRAINT [FK__PlayField__Booki__59063A47]
GO
ALTER TABLE [dbo].[SystemFeedback]  WITH CHECK ADD  CONSTRAINT [FK__SystemFeedbacj__User__59063A47] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[SystemFeedback] CHECK CONSTRAINT [FK__SystemFeedbacj__User__59063A47]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK__User__RoleID__59FA5E80] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK__User__RoleID__59FA5E80]
GO
ALTER TABLE [dbo].[UserClub]  WITH CHECK ADD  CONSTRAINT [FK__UserClub__ClubId__5AEE82B9] FOREIGN KEY([ClubId])
REFERENCES [dbo].[Club] ([ClubId])
GO
ALTER TABLE [dbo].[UserClub] CHECK CONSTRAINT [FK__UserClub__ClubId__5AEE82B9]
GO
ALTER TABLE [dbo].[UserClub]  WITH CHECK ADD  CONSTRAINT [FK__UserClub__UserID__5BE2A6F2] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserClub] CHECK CONSTRAINT [FK__UserClub__UserID__5BE2A6F2]
GO
ALTER TABLE [dbo].[UserMeeting]  WITH CHECK ADD  CONSTRAINT [FK__UserMeeti__Meeti__5CD6CB2B] FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meeting] ([MeetingID])
GO
ALTER TABLE [dbo].[UserMeeting] CHECK CONSTRAINT [FK__UserMeeti__Meeti__5CD6CB2B]
GO
ALTER TABLE [dbo].[UserMeeting]  WITH CHECK ADD  CONSTRAINT [FK__UserMeeti__UserI__5DCAEF64] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserMeeting] CHECK CONSTRAINT [FK__UserMeeti__UserI__5DCAEF64]
GO
ALTER TABLE [dbo].[UserSport]  WITH CHECK ADD  CONSTRAINT [FK__UserSport__Sport__5EBF139D] FOREIGN KEY([SportID])
REFERENCES [dbo].[Sport] ([SportID])
GO
ALTER TABLE [dbo].[UserSport] CHECK CONSTRAINT [FK__UserSport__Sport__5EBF139D]
GO
ALTER TABLE [dbo].[UserSport]  WITH CHECK ADD  CONSTRAINT [FK__UserSport__UserI__5FB337D6] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserSport] CHECK CONSTRAINT [FK__UserSport__UserI__5FB337D6]
GO