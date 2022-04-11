import { HttpClient } from "aurelia";
import { IRefreshToken } from "../../domain/identity/IRefreshToken";
import { IUser } from "../../domain/identity/IUser";
import { IStorage } from "../../domain/inventory/IStorage";

export class InventoryRepository {
    httpClient: HttpClient = new HttpClient();
    constructor() {

    }

    async getAllStorages(user: IUser): Promise<IStorage[]> {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.append('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })
        let response = await this.httpClient.get(`https://localhost:7286/api/storage`, { cache: "no-store" });
        let json = await response.json();
        let data: IStorage[] = json;

        console.log("https://localhost:7286/api/identity/authentication/get -> " + data);
        return data;
    }

}