import { HttpClient } from "aurelia";
import { IUser } from "../../domain/identity/IUser";
import { IStorage } from "../../domain/inventory/IStorage";
import { I_Item } from "../../domain/inventory/I_Item";

export class InventoryService {
    httpClient: HttpClient = new HttpClient();
    constructor() {

    }

    async getAllStorages(user: IUser): Promise<IStorage[]> {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })
        let response = await this.httpClient.get(`https://localhost:7286/api/storage`);
        let json = await response.json();
        let data: IStorage[] = json;

        console.log("https://localhost:7286/api/identity/authentication/get -> ", data);
        return data;
    }

    async getStorageById(id: string, user: IUser): Promise<IStorage> {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })

        let response = await this.httpClient.get(`https://localhost:7286/api/storage/` + id);
        let json = await response.json();
        let data: IStorage = json;

        console.log("https://localhost:7286/api/identity/authentication/get/id -> ", data);
        return data;

    }

    async deleteStorage(id: string, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })

        await this.httpClient.delete(`https://localhost:7286/api/storage/` + id);
    }

    async editStorage(storage: IStorage, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })

        await this.httpClient.put(`https://localhost:7286/api/storage/`, JSON.stringify(storage));
    }

    async getItemById(id: string, user: IUser): Promise<I_Item | undefined> {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })

        let response = await this.httpClient.get(`https://localhost:7286/api/storageItem/` + id);
        let json = await response.json();
        let data: I_Item = json;

        console.log("https://localhost:7286/api/storageItem/id -> ", data);
        return data;
    }

    async getItemsByStorageId(id: string, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })

        await this.httpClient.get(`https://localhost:7286/api/storage/` + id);
    }

    async deleteItem(id: string, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.jwt);
                    return request;
                }
            });
        })

        await this.httpClient.delete(`https://localhost:7286/api/storageItem/` + id);
    }
}