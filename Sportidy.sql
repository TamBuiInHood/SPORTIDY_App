USE [Sportidy]
GO
/****** Object:  Table [dbo].[Booking]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Booking](
	[BookingID] [int] NOT NULL,
	[BookingCode] [int] NULL,
	[BookingDate] [date] NULL,
	[Price] [int] NULL,
	[DateStart] [date] NULL,
	[DateEnd] [date] NULL,
	[Status] [int] NULL,
	[PaymentMethod] [int] NULL,
	[BarCode] [int] NULL,
	[PlayFieldID] [int] NOT NULL,
	[Description] [text] NULL,
	[CustomerID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Club]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Club](
	[ClubId] [int] NOT NULL,
	[ClubCode] [varchar](36) NULL,
	[ClubName] [varchar](max) NULL,
	[Regulation] [varchar](max) NULL,
	[Infomation] [varchar](max) NULL,
	[Slogan] [varchar](200) NULL,
	[MainSport] [varchar](100) NULL,
	[CreateDate] [date] NULL,
	[Location] [varchar](200) NULL,
	[TotalMember] [int] NULL,
	[AvartarClub] [varchar](1) NULL,
	[CoverImageClub] [varchar](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[ClubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommentInMeeting]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommentInMeeting](
	[CommentID] [int] NOT NULL,
	[CommentCode] [varchar](36) NULL,
	[CommentDate] [date] NULL,
	[UserID] [int] NOT NULL,
	[Content] [varchar](max) NULL,
	[Image] [varchar](max) NULL,
	[MeetingID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Friendship]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friendship](
	[FriendShipID] [int] NOT NULL,
	[FriendShipCode] [varchar](36) NULL,
	[Status] [int] NULL,
	[CreateDate] [date] NULL,
	[UserID1] [int] NOT NULL,
	[UserID2] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FriendShipID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ImageField]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ImageField](
	[ImageID] [int] NOT NULL,
	[ImageURL] [varchar](300) NULL,
	[VideoURL] [int] NULL,
	[IsSportlight] [bit] NULL,
	[PlayFieldID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meeting]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meeting](
	[MeetingID] [int] NOT NULL,
	[MeetingCode] [varchar](36) NULL,
	[MeetingName] [varchar](100) NULL,
	[MeetingImage] [varchar](300) NULL,
	[Address] [varchar](max) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Host] [int] NULL,
	[TotalMember] [int] NULL,
	[ClubID] [int] NULL,
	[Note] [varchar](max) NULL,
	[IsPublic] [int] NULL,
	[SportID] [int] NULL,
	[CancelBefore] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationID] [int] NOT NULL,
	[NotificationCode] [varchar](36) NULL,
	[Tiltle] [varchar](1) NULL,
	[Message] [varchar](1) NULL,
	[NotificationType] [bit] NULL,
	[IsAccept] [bit] NULL,
	[InviteDate] [date] NULL,
	[IsRead] [bit] NULL,
	[UserID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayField]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayField](
	[PlayFieldID] [int] NOT NULL,
	[PlayFieldCode] [varchar](36) NULL,
	[PlayFieldName] [varchar](200) NULL,
	[Price] [int] NULL,
	[Address] [varchar](200) NULL,
	[OpenTime] [date] NULL,
	[UserID] [int] NULL,
	[CloseTime] [date] NULL,
 CONSTRAINT [PK__PlayFiel__4E6EFC93249A0D85] PRIMARY KEY CLUSTERED 
(
	[PlayFieldID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlayFieldFeedback]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayFieldFeedback](
	[FeedbackID] [int] NOT NULL,
	[FeedbackCode] [varchar](36) NULL,
	[Content] [varchar](max) NULL,
	[Rating] [int] NULL,
	[FeedbackDate] [int] NULL,
	[ImageURL] [int] NULL,
	[VideoURL] [int] NULL,
	[IsAnonymous_] [bit] NULL,
	[BookingID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FeedbackID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleName] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sport]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sport](
	[SportID] [int] NOT NULL,
	[SportCode] [int] NULL,
	[SportName] [int] NULL,
	[SportIamge] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[SportID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] NOT NULL,
	[UserCode] [varchar](36) NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
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
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClub]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClub](
	[IsLeader] [bit] NULL,
	[UserID] [int] NOT NULL,
	[ClubId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[ClubId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMeeting]    Script Date: 8/14/2024 9:36:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMeeting](
	[ClubID] [int] NULL,
	[IsHost] [bit] NULL,
	[UserID] [int] NOT NULL,
	[MeetingID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[MeetingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSport]    Script Date: 8/14/2024 9:36:03 PM ******/
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
ALTER TABLE [dbo].[Booking]  WITH CHECK ADD  CONSTRAINT [FK__Booking__PlayFie__5070F446] FOREIGN KEY([PlayFieldID])
REFERENCES [dbo].[PlayField] ([PlayFieldID])
GO
ALTER TABLE [dbo].[Booking] CHECK CONSTRAINT [FK__Booking__PlayFie__5070F446]
GO
ALTER TABLE [dbo].[CommentInMeeting]  WITH CHECK ADD FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meeting] ([MeetingID])
GO
ALTER TABLE [dbo].[Friendship]  WITH CHECK ADD FOREIGN KEY([UserID1])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Friendship]  WITH CHECK ADD FOREIGN KEY([UserID2])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[ImageField]  WITH CHECK ADD  CONSTRAINT [FK__ImageFiel__PlayF__4316F928] FOREIGN KEY([PlayFieldID])
REFERENCES [dbo].[PlayField] ([PlayFieldID])
GO
ALTER TABLE [dbo].[ImageField] CHECK CONSTRAINT [FK__ImageFiel__PlayF__4316F928]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PlayField]  WITH CHECK ADD  CONSTRAINT [FK_PlayField_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[PlayField] CHECK CONSTRAINT [FK_PlayField_User]
GO
ALTER TABLE [dbo].[PlayFieldFeedback]  WITH CHECK ADD FOREIGN KEY([BookingID])
REFERENCES [dbo].[Booking] ([BookingID])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[UserClub]  WITH CHECK ADD FOREIGN KEY([ClubId])
REFERENCES [dbo].[Club] ([ClubId])
GO
ALTER TABLE [dbo].[UserClub]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserMeeting]  WITH CHECK ADD FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meeting] ([MeetingID])
GO
ALTER TABLE [dbo].[UserMeeting]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[UserSport]  WITH CHECK ADD FOREIGN KEY([SportID])
REFERENCES [dbo].[Sport] ([SportID])
GO
ALTER TABLE [dbo].[UserSport]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
