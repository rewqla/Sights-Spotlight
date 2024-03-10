import axios, { AxiosResponse } from "axios";

axios.defaults.baseURL = "https://localhost:7188/api";
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;

const responseBody = (response: AxiosResponse) => response.data;

const Account = {
  login: (values: any) =>
    axios.post("account/login", values).then(responseBody),
  register: (values: any) => {
    return axios.post("account/register", values).then(responseBody);
  },
};

const Country = {
  countries: () => axios.get("country/countries").then(responseBody),
  countryDetails: (id: number) => axios.get(`country/${id}`).then(responseBody),
};

const agent = {
  Account,
  Country,
};

export default agent;
