import axios from "axios";

axios.defaults.baseURL = "https://localhost:7188/api";
// axios.defaults.headers.common['Authorization'] = AUTH_TOKEN;

const Account = {
  login: (values: any) =>
    axios.post("account/login", values).then((response) => response.data),
  register: (values: any) => {
    return axios
      .post("account/register", values)
      .then((response) => response.data);
  },
};

const Country = {
  countries: () =>
    axios.get("country/countries").then((response) => response.data),
};

const agent = {
  Account,
  Country,
};

export default agent;
