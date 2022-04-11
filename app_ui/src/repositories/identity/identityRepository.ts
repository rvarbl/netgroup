import { HttpClient } from "aurelia";
import { config } from "process";
import { ILogin } from "../../domain/identity/ILogin";
import { IRefreshToken } from "../../domain/identity/IRefreshToken";
import { IRegister } from "../../domain/identity/IRegister";
import { IUser } from "../../domain/identity/IUser";

export class IdentityRepository {
    httpClient: HttpClient = new HttpClient();
    constructor() {



        this.get();

    }

    //suAdmin@test.ee

    async logIn(login: ILogin): Promise<IUser> {

        console.log("Identity repo asdasdasdadsd " + this.httpClient);
        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/login", JSON.stringify(login))
        let json = await response.json();
        let user: IUser = {
            email: json["email"],
            firstName: json["firstName"],
            lastName: json["lastName"],
            jwt: json["token"],
            refreshToken: json["refreshToken"],
            role: "user"
        }
        //validate user
        //check and add role

        console.log("https://localhost:7286/api/identity/authentication/login " + user);

        return user;
    }



    async logOut(user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.append('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })
        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/revoke")
        console.log("https://localhost:7286/api/identity/authentication/revoke " + response.status);

    }

    async refreshToken(user: IUser) {
        let dto: IRefreshToken = { jwt: user.jwt, refreshToken: user.refreshToken }


        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/refreshToken", JSON.stringify(dto))
        console.log("https://localhost:7286/api/identity/authentication/refreshToken " + response.status);
    }

    async register(registerDto: IRegister): Promise<IUser> {
        let response = await this.httpClient.post("https://localhost:7286/api/identity/authentication/register", JSON.stringify(registerDto))
        let json = await response.json();

        let user: IUser = {
            email: json["email"],
            firstName: json["firstName"],
            lastName: json["lastName"],
            jwt: json["token"],
            refreshToken: json["refreshToken"],
            role: "user"
        }
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