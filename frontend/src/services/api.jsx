import axios from "axios";
import history from "../lib/history";

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL,
});

api.interceptors.request.use(
  async (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  async (error) => {
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error?.request && error?.response == undefined) {
      history.push("/unavailable");
    }

    if (error?.response?.status == 401 || error?.response?.status == 403) {
      history.push("/unauthorized");
    }

    return Promise.reject(error);
  }
);

export default api;
