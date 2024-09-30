export type RootStackParamList = {
  Splash: any;
  SignUp: any;
  Login: any;
  ForgotPassword: any;
  Verification: any;
  NewPassword: any;
  UserProfile: any;
  Tabs: any;
};

export interface Card{
  meetingId: number;
  meetingCode: string;
  meetingName: string | null;
  meetingImage: string;
  address: string;
  startDate: string; // or Date
  endDate: string; // or Date
  host: number;
  totalMember: number;
  clubId: number | null;
  note: string;
  isPublic: boolean;
}

export interface MeetingsResponse {
  meetings: Card[];
}