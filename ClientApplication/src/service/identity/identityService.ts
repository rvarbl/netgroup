import { HttpClient } from "aurelia";
import { ILogin } from "../../domain/identity/ILogin";
import { IRefreshToken } from "../../domain/identity/IRefreshToken";
import { IRegister } from "../../domain/identity/IRegister";
import { IUser } from "../../domain/identity/IUser";

export class IdentityService {
    httpClient: HttpClient = new HttpClient();
    constructor() {
        this.get();
    }

    //suAdmin@test.ee

    async logIn(login: ILogin): Promise<IUser> {
        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/login", JSON.stringify(login))
        let json = await response.json();
        let user = json;

        //validate user
        //check and add role

        console.log("https://localhost:7286/api/identity/authentication/login " + user);

        return user;
    }

    async logOut(user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.append('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })
        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/revoke")
        console.log("https://localhost:7286/api/identity/authentication/revoke " + response.status);

    }

    async refreshToken(user: IUser) {
        let dto: IRefreshToken = { jwt: user.token, refreshToken: user.refreshToken }

        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/refreshToken", JSON.stringify(dto))
        let json = await response.json();
        let refreshToken: IRefreshToken = json;
        user.token = refreshToken["jwt"];
        user.refreshToken = refreshToken["refreshToken"];
        
        return user;
    }

    async register(registerDto: IRegister): Promise<IUser> {
        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/register", JSON.stringify(registerDto))
        let json = await response.json();
        let user = json;

        //validate user
        //check and add role

        console.log("https://localhost:7286/api/identity/authentication/register " + user.email);
        return user;
    }


    private async get() {
        let response = await this.httpClient.fetch(`https://localhost:7286/api/identity/authentication/get`, { cache: "no-store" });
        let status = response.status;
        console.log("https://localhost:7286/api/identity/authentication/get -> " + status);
    }
}