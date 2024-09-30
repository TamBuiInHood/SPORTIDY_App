import { MeetingsResponse } from '@/types/types';
import axiosClient from './axios';

const api = {
  getAllMeeting: (pageNumber: number, pageSize: number) => {
    const url = `sportidy/meetings?page-number=${pageNumber}&page-size=${pageSize}`;
    return axiosClient.get<MeetingsResponse>(url)
      .then(response => response.data); // Extract the data here
  },
};

export default api;
