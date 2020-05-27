import axios, { AxiosInstance } from "axios";

class ApiCaller {
	port: string;
	baseURL: string;
	axiosInstance: AxiosInstance;
	constructor(gwPort: string) {
		this.port = gwPort;
		this.baseURL = `https://localhost:${this.port}/api`;
		this.axiosInstance = axios;
		this.axiosInstance.defaults.baseURL = this.baseURL;
	}
	async get(path: string) {
		let response = await this.axiosInstance.get(path);

		return response;
	}
}
export default ApiCaller;