export type RootStackParamList = {
  Splash: any;
  SignUp: any;
  Login: any;
  ForgotPassword: any;
  Verification: any;
  NewPassword: any;
  UserProfile: any;
  '(tabs)': {
    params: {
      screen: any;
    };
  };
  PlayField: any;
  
};
export type TabParamList = {
  index: undefined;
  booking: undefined;
  create: undefined;
  club: undefined;
  account: undefined;
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
  isPublic: boolean;
}

export interface MeetingsResponse {
  meetings: Card[];
}

export interface Booking{
  bookingId: number,
  bookingCode: string,
  bookingDate: Date,
  price: number,
  dateStart: Date,
  dateEnd: Date,
  status: number,
  barcode: string,
  description: string,
  customerId: number,
  playField: PlayField
}

export interface PlayField {
  playFieldId: number,
  playFieldName: string,
  playFieldCode: string,
  price: number,
  address: string,
  openTime: string,
  closeTime: string,
  avatarImage: string,
  status: number,
  sportId: number,
}