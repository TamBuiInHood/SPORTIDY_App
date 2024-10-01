import axios from 'axios';
import Config from 'react-native-config';
import queryString from "query-string";

// Create an instance of axios
const axiosClient = axios.create({
  baseURL: 'http://192.168.1.8',
  headers: {
    'Content-Type': 'application/json',
  },
  paramsSerializer: (params) => queryString.stringify(params),
});

axiosClient.interceptors.request.use(
  (config) => {
    console.log("Request config:", config);
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axiosClient.interceptors.response.use(
    (response) => {
        if (response && response.data) {
          return response.data;
        }
        return response.data;
      },
  (error) => {
    // Handle global error scenarios (e.g., 401, 500)
    return Promise.reject(error);
  }
);
export default axiosClient;