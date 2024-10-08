USE [master]
GO
DROP DATABASE [Sportidy]
GO
CREATE DATABASE [Sportidy]
GO
USE [Sportidy]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Booking]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Club]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[CommentInMeeting]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Friendship]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[ImageField]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Meeting]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Notification]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Payment]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[PlayField]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[PlayFieldFeedback]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[Sport]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[SystemFeedback]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[UserClub]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[UserMeeting]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[UserSport]    Script Date: 9/26/2024 12:28:57 PM ******/
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
/****** Object:  Table [dbo].[UserToken]    Script Date: 9/26/2024 12:28:57 PM ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240913035746_init', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240918035356_addpayment', N'8.0.8')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240918061623_chageNotification', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 
GO
INSERT [dbo].[Booking] ([BookingID], [BookingCode], [BookingDate], [Price], [DateStart], [DateEnd], [Status], [PaymentMethod], [BarCode], [PlayFieldID], [Description], [CustomerID]) VALUES (1, N'B001', CAST(N'2024-09-01T00:00:00.000' AS DateTime), 500, CAST(N'2024-09-01T00:00:00.000' AS DateTime), CAST(N'2024-09-01T00:00:00.000' AS DateTime), 1, N'CreditCard', N'123456789', 1, N'Football booking', 2)
GO
INSERT [dbo].[Booking] ([BookingID], [BookingCode], [BookingDate], [Price], [DateStart], [DateEnd], [Status], [PaymentMethod], [BarCode], [PlayFieldID], [Description], [CustomerID]) VALUES (2, N'B002', CAST(N'2024-09-02T00:00:00.000' AS DateTime), 300, CAST(N'2024-09-03T00:00:00.000' AS DateTime), CAST(N'2024-09-03T00:00:00.000' AS DateTime), 2, N'VNPay', N'987654321', 2, N'Basketball booking', 2)
GO
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Club] ON 
GO
INSERT [dbo].[Club] ([ClubId], [ClubCode], [ClubName], [Regulation], [Infomation], [Slogan], [MainSport], [CreateDate], [Location], [TotalMember], [AvartarClub], [CoverImageClub]) VALUES (1, N'C001', N'Football Club', N'Regulation text', N'Club info', N'Teamwork wins', N'Football', CAST(N'2024-01-01' AS Date), N'Hanoi', 50, N'avatar1.jpg', N'cover1.jpg')
GO
INSERT [dbo].[Club] ([ClubId], [ClubCode], [ClubName], [Regulation], [Infomation], [Slogan], [MainSport], [CreateDate], [Location], [TotalMember], [AvartarClub], [CoverImageClub]) VALUES (2, N'C002', N'Basketball Club', N'Regulation text', N'Club info', N'Together we play', N'Basketball', CAST(N'2024-02-01' AS Date), N'Saigon', 30, N'avatar2.jpg', N'cover2.jpg')
GO
SET IDENTITY_INSERT [dbo].[Club] OFF
GO
SET IDENTITY_INSERT [dbo].[CommentInMeeting] ON 
GO
INSERT [dbo].[CommentInMeeting] ([CommentID], [CommentCode], [CommentDate], [UserID], [Content], [Image], [MeetingID]) VALUES (1, N'CM001', CAST(N'2024-09-01T00:00:00.000' AS DateTime), 1, N'This is a comment.', N'comment1.jpg', 1)
GO
INSERT [dbo].[CommentInMeeting] ([CommentID], [CommentCode], [CommentDate], [UserID], [Content], [Image], [MeetingID]) VALUES (2, N'CM002', CAST(N'2024-09-02T00:00:00.000' AS DateTime), 2, N'Another comment.', N'comment2.jpg', 2)
GO
SET IDENTITY_INSERT [dbo].[CommentInMeeting] OFF
GO
SET IDENTITY_INSERT [dbo].[Friendship] ON 
GO
INSERT [dbo].[Friendship] ([FriendShipID], [FriendShipCode], [Status], [CreateDate], [UserID1], [UserID2], [RequestBy]) VALUES (1, N'F001', 1, CAST(N'2024-01-01' AS Date), 1, 2, 1)
GO
INSERT [dbo].[Friendship] ([FriendShipID], [FriendShipCode], [Status], [CreateDate], [UserID1], [UserID2], [RequestBy]) VALUES (2, N'F002', 0, CAST(N'2024-01-02' AS Date), 2, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[Friendship] OFF
GO
SET IDENTITY_INSERT [dbo].[ImageField] ON 
GO
INSERT [dbo].[ImageField] ([ImageID], [ImageURL], [VideoURL], [ImageIndex], [PlayFieldID]) VALUES (1, N'image1.jpg', N'video1.mp4', 1, 1)
GO
INSERT [dbo].[ImageField] ([ImageID], [ImageURL], [VideoURL], [ImageIndex], [PlayFieldID]) VALUES (2, N'image2.jpg', N'video2.mp4', 2, 2)
GO
SET IDENTITY_INSERT [dbo].[ImageField] OFF
GO
SET IDENTITY_INSERT [dbo].[Meeting] ON 
GO
INSERT [dbo].[Meeting] ([MeetingID], [MeetingCode], [MeetingName], [MeetingImage], [Address], [StartDate], [EndDate], [Host], [TotalMember], [ClubID], [Note], [IsPublic], [SportID], [CancelBefore], [Status]) VALUES (1, N'M001', N'Football Match', N'meeting1.jpg', N'Stadium 1', CAST(N'2024-09-10T00:00:00.000' AS DateTime), CAST(N'2024-09-10T00:00:00.000' AS DateTime), 1, 22, 1, N'First match', 1, 1, 1, 1)
GO
INSERT [dbo].[Meeting] ([MeetingID], [MeetingCode], [MeetingName], [MeetingImage], [Address], [StartDate], [EndDate], [Host], [TotalMember], [ClubID], [Note], [IsPublic], [SportID], [CancelBefore], [Status]) VALUES (2, N'M002', N'Basketball Game', N'meeting2.jpg', N'Court 1', CAST(N'2024-09-12T00:00:00.000' AS DateTime), CAST(N'2024-09-12T00:00:00.000' AS DateTime), 2, 15, 2, N'Friendly match', 0, 2, 2, 0)
GO
SET IDENTITY_INSERT [dbo].[Meeting] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 
GO
INSERT [dbo].[Notification] ([NotificationID], [NotificationCode], [Tiltle], [Message], [NotificationType], [IsAccept], [InviteDate], [IsRead], [UserID]) VALUES (1, N'N001', N'Invitation', N'You have been invited', 1, 1, CAST(N'2024-09-01T00:00:00.000' AS DateTime), 0, 1)
GO
INSERT [dbo].[Notification] ([NotificationID], [NotificationCode], [Tiltle], [Message], [NotificationType], [IsAccept], [InviteDate], [IsRead], [UserID]) VALUES (2, N'N002', N'Reminder', N'Don’t forget the meeting', 0, 0, CAST(N'2024-09-02T00:00:00.000' AS DateTime), 1, 2)
GO
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
SET IDENTITY_INSERT [dbo].[Payment] ON 
GO
INSERT [dbo].[Payment] ([PaymentId], [OrderCode], [Amount], [DateOfTransaction], [Status], [BookingId]) VALUES (1, N'O001', 1000, CAST(N'2024-09-01T00:00:00.000' AS DateTime), 1, 1)
GO
INSERT [dbo].[Payment] ([PaymentId], [OrderCode], [Amount], [DateOfTransaction], [Status], [BookingId]) VALUES (2, N'O002', 500, CAST(N'2024-09-02T00:00:00.000' AS DateTime), 0, 2)
GO
SET IDENTITY_INSERT [dbo].[Payment] OFF
GO
SET IDENTITY_INSERT [dbo].[PlayField] ON 
GO
INSERT [dbo].[PlayField] ([PlayFieldID], [PlayFieldCode], [PlayFieldName], [Price], [Address], [OpenTime], [UserID], [IsDependency], [CloseTime], [AvatarImage], [SportId], [Status]) VALUES (1, N'P001', N'Football Field 1', 200, N'Hanoi', CAST(N'08:00:00' AS Time), 1, NULL, CAST(N'20:00:00' AS Time), N'field1.jpg', NULL, 1)
GO
INSERT [dbo].[PlayField] ([PlayFieldID], [PlayFieldCode], [PlayFieldName], [Price], [Address], [OpenTime], [UserID], [IsDependency], [CloseTime], [AvatarImage], [SportId], [Status]) VALUES (2, N'P002', N'Basketball Court 1', 150, N'Saigon', CAST(N'09:00:00' AS Time), 2, NULL, CAST(N'19:00:00' AS Time), N'field2.jpg', NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[PlayField] OFF
GO
SET IDENTITY_INSERT [dbo].[PlayFieldFeedback] ON 
GO
INSERT [dbo].[PlayFieldFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [BookingID]) VALUES (1, N'F001', N'Great field', 5, CAST(N'2024-09-01T00:00:00.000' AS DateTime), N'feedback1.jpg', N'video1.mp4', 0, 1)
GO
INSERT [dbo].[PlayFieldFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [BookingID]) VALUES (2, N'F002', N'Good service', 4, CAST(N'2024-09-02T00:00:00.000' AS DateTime), N'feedback2.jpg', N'video2.mp4', 1, 2)
GO
INSERT [dbo].[PlayFieldFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [BookingID]) VALUES (3, N'F001', N'Great field', 5, CAST(N'2024-09-01T00:00:00.000' AS DateTime), N'feedback1.jpg', N'video1.mp4', 0, 1)
GO
INSERT [dbo].[PlayFieldFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [BookingID]) VALUES (4, N'F002', N'Good service', 4, CAST(N'2024-09-02T00:00:00.000' AS DateTime), N'feedback2.jpg', N'video2.mp4', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[PlayFieldFeedback] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Admin')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'User')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (3, N'Admin')
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (4, N'User')
GO
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Sport] ON 
GO
INSERT [dbo].[Sport] ([SportID], [SportCode], [SportName], [SportImage]) VALUES (1, N'SP001', N'Football', N'football.jpg')
GO
INSERT [dbo].[Sport] ([SportID], [SportCode], [SportName], [SportImage]) VALUES (2, N'SP002', N'Basketball', N'basketball.jpg')
GO
INSERT [dbo].[Sport] ([SportID], [SportCode], [SportName], [SportImage]) VALUES (3, N'SP001', N'Football', N'football.jpg')
GO
INSERT [dbo].[Sport] ([SportID], [SportCode], [SportName], [SportImage]) VALUES (4, N'SP002', N'Basketball', N'basketball.jpg')
GO
SET IDENTITY_INSERT [dbo].[Sport] OFF
GO
SET IDENTITY_INSERT [dbo].[SystemFeedback] ON 
GO
INSERT [dbo].[SystemFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [UserID]) VALUES (1, N'SF001', N'System works well', 5, CAST(N'2024-09-01T00:00:00.000' AS DateTime), N'system1.jpg', N'video1.mp4', 0, 1)
GO
INSERT [dbo].[SystemFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [UserID]) VALUES (2, N'SF002', N'Minor bugs found', 3, CAST(N'2024-09-02T00:00:00.000' AS DateTime), N'system2.jpg', N'video2.mp4', 1, 2)
GO
INSERT [dbo].[SystemFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [UserID]) VALUES (3, N'SF001', N'System works well', 5, CAST(N'2024-09-01T00:00:00.000' AS DateTime), N'system1.jpg', N'video1.mp4', 0, 1)
GO
INSERT [dbo].[SystemFeedback] ([FeedbackID], [FeedbackCode], [Content], [Rating], [FeedbackDate], [ImageURL], [VideoURL], [IsAnonymous_], [UserID]) VALUES (4, N'SF002', N'Minor bugs found', 3, CAST(N'2024-09-02T00:00:00.000' AS DateTime), N'system2.jpg', N'video2.mp4', 1, 2)
GO
SET IDENTITY_INSERT [dbo].[SystemFeedback] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserID], [UserCode], [UserName], [Password], [FullName], [Address], [OTP], [Email], [Gender], [Description], [Avartar], [Birtday], [RoleID], [CreateDate], [Status], [IsDeleted], [Phone], [BankCode], [BankName], [DeviceCode]) VALUES (1, N'U001', N'john_doe', N'password123', N'John Doe', N'123 Main St', N'123456', N'john@example.com', 1, N'Regular user', N'avatar1.jpg', CAST(N'1990-01-01' AS Date), 1, CAST(N'2024-09-01' AS Date), 1, 0, N'0123456789', N'BANK001', N'Bank A', NULL)
GO
INSERT [dbo].[User] ([UserID], [UserCode], [UserName], [Password], [FullName], [Address], [OTP], [Email], [Gender], [Description], [Avartar], [Birtday], [RoleID], [CreateDate], [Status], [IsDeleted], [Phone], [BankCode], [BankName], [DeviceCode]) VALUES (2, N'U002', N'jane_doe', N'password456', N'Jane Doe', N'456 High St', N'789012', N'jane@example.com', 2, N'VIP user', N'avatar2.jpg', CAST(N'1992-02-02' AS Date), 2, CAST(N'2024-09-02' AS Date), 1, 0, N'0987654321', N'BANK002', N'Bank B', NULL)
GO
INSERT [dbo].[User] ([UserID], [UserCode], [UserName], [Password], [FullName], [Address], [OTP], [Email], [Gender], [Description], [Avartar], [Birtday], [RoleID], [CreateDate], [Status], [IsDeleted], [Phone], [BankCode], [BankName], [DeviceCode]) VALUES (3, N'U001', N'john_doe', N'password123', N'John Doe', N'123 Main St', N'123456', N'john@example.com', 1, N'Regular user', N'avatar1.jpg', CAST(N'1990-01-01' AS Date), 1, CAST(N'2024-09-01' AS Date), 1, 0, N'0123456789', N'BANK001', N'Bank A', NULL)
GO
INSERT [dbo].[User] ([UserID], [UserCode], [UserName], [Password], [FullName], [Address], [OTP], [Email], [Gender], [Description], [Avartar], [Birtday], [RoleID], [CreateDate], [Status], [IsDeleted], [Phone], [BankCode], [BankName], [DeviceCode]) VALUES (4, N'U002', N'jane_doe', N'password456', N'Jane Doe', N'456 High St', N'789012', N'jane@example.com', 2, N'VIP user', N'avatar2.jpg', CAST(N'1992-02-02' AS Date), 2, CAST(N'2024-09-02' AS Date), 1, 0, N'0987654321', N'BANK002', N'Bank B', NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
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
ALTER TABLE [dbo].[PlayField]  WITH CHECK ADD  CONSTRAINT [FK_PlayField_Sport] FOREIGN KEY([SportId])
REFERENCES [dbo].[Sport] ([SportID])
GO
ALTER TABLE [dbo].[PlayField] CHECK CONSTRAINT [FK_PlayField_Sport]
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