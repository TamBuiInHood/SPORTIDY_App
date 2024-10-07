import { Card, Club,  MeetingsResponse, PlayField, Sport } from '@/types/types';
import axiosClient from './axios';
import { AxiosResponse } from 'axios';

const api = {
  //MEETING
  getAllMeeting: (pageNumber: number, pageSize: number): Promise<AxiosResponse<MeetingsResponse>> => {
    const url = `/sportidy/meetings?page-number=${pageNumber}&page-size=${pageSize}`;
    return axiosClient.get(url);
  },
  getMeetingById: (meetingId: number) => {
    const url = `/sportidy/meetings/${meetingId}`;
    return axiosClient.get<Card>(url)
  },
  joinMeeting: (userId: number, clubId: number, meetingId: number) => {
    const url = `/sportidy/meetings/engage-to-meeting`;
    return axiosClient.post(url, {
      userId: userId,
      clubId: clubId,
      meetingId: meetingId
    })
  },
  createMeeting:  (meetingData:any) => {
    const url = `/sportidy/meetings`;
    return axiosClient.post(url, {
     meetingData
    })
  },
  //COMMENTS
  getComments: (meetingId: number) => {
    const url = `/sportidy/${meetingId}/comments`;
    return axiosClient.get<Comment>(url)
  },
  webSocket: () => {
    const url = `/sportidywebsocket`;
    return axiosClient.get(url);
  },
  createComment: (userId: number, content: string, image: string, meetingId: number) => {
    const url = `/sportidy/comments`;
    return axiosClient.post(url, {
      userId: userId,
      content: content,
      image: image,
      meetingId: meetingId
    });
  },
  updateComment: () => {
    const url = '/sportidy/comments';
    return axiosClient.put(url);
  },
  //PLAYFIELD
  getAllPlayField: (pageIndex: number, pageSize: number) => {
    const url = `/sportidy/Playfields?pageIndex=${pageIndex}&pageSize=${pageSize}`;
    return axiosClient.get<PlayField>(url);
  },
  getPlayfieldById: (playFieldId: number) => {
    const url = `/sportidy/PlayFields/${playFieldId}`;
    return axiosClient.get<PlayField>(url)
  },
  //BOOKING
  createBooking: () => {
    const url = `/sportidy/bookings`;
    return axiosClient.post(url);
  },
  getBookingById: (bookingId: number) => {
    const url = `/sportidy/bookings/${bookingId}`;
    return axiosClient.get(url);
  },
  //PAYMENT 
  createPayment: () => {
    const url = `/sportidy/payment/create-payment-link`;
    return axiosClient.post(url);
  },
  //SPORTS
  getAllSports: () => {
    const url = `/sportidy/sports/get-all-not-paging`;
    return axiosClient.get<Sport>(url);
  },
  getSportById: (sportId: number) => {
    const url = `/sportidy/sports/${sportId}`;
    return axiosClient.get(url);
  },
  createSport: () => {
    const url = `/sportidy/sports`;
    return axiosClient.post(url);
  },
  //CLUB
  getClubById: (clubId: number) => {
    const url = `/sportidy/clubs/${clubId}`;
    return axiosClient.get<Club>(url);
  },
  getClubByUserId: (userId: number) => {
    const url = `/sportidy/clubs/joined-club/${userId}`;
    return axiosClient.get<Club>(url);
  }
};

export default api;
