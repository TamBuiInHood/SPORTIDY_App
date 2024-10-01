import { MeetingsResponse } from '@/types/types';
import axiosClient from './axios';

const api = {
  //MEETING
  getAllMeeting: (pageNumber: number, pageSize: number) => {
    const url = `/sportidy/meetings?page-number=${pageNumber}&page-size=${pageSize}`;
    return axiosClient.get<MeetingsResponse>(url)
      .then(response => response.data); // Extract the data here
  },
  getMeetingById: (meetingId: number) => {
    const url = `/sportidy/meeting/${meetingId}`;
    return axiosClient.get<MeetingsResponse>(url)
  },
  joinMeeting: () => {
    const url = `/sportidy/meeting/engage-to-meeting`;
    return axiosClient.post(url)
  },
  //COMMENTS
  getComments: (meetingId: number) => {
    const url = `/sportidy${meetingId}/comments`;
    return axiosClient.get(url)
  },
  webSocket: () => {
    const url = `/sportidywebsocket`;
    return axiosClient.get(url);
  },
  createComment: () => {
    const url = `/sportidy/comments`;
    return axiosClient.post(url);
  },
  updateComment: () => {
    const url = '/sportidy/comments';
    return axiosClient.put(url);
  },
  //PLAYFIELD
  getAllPlayField: () => {
    const url = `/sportidy/Playfields`;
    return axiosClient.get(url);
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
  getAllSports: () =>{
    const url = `/sportidy/sports/get-all-not-paging`;
    return axiosClient.get(url);
  },
  getSportById: (sportId:number) =>{
    const url = `/sportidy/sports/${sportId}`;
    return axiosClient.get(url);
  },
  createSport: () =>{
    const url = `/sportidy/sports`;
    return axiosClient.post(url);
  },
};

export default api;
