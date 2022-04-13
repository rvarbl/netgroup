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
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
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
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
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
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        await this.httpClient.delete(`https://localhost:7286/api/storage/` + id);
    }

    async createStorage(storage: IStorage, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })
        console.log("POSTING: ", JSON.stringify(storage))
        let response = await this.httpClient.post(`https://localhost:7286/api/storage/`, JSON.stringify(storage));
        return response.status;
    }

    async editStorage(storage: IStorage, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })
        console.log("EDITING3: ", storage);
        return await this.httpClient.put(`https://localhost:7286/api/storage/`, JSON.stringify(storage));
    }
    async editItem(item: I_Item, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })
        console.log("EDITING3: ", item);
        return await this.httpClient.put(`https://localhost:7286/api/storageItem/` + item.id, JSON.stringify(item));
    }

    async getItemById(id: string, user: IUser): Promise<I_Item | undefined> {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
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
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        await this.httpClient.get(`https://localhost:7286/api/storage/` + id);
    }
    async createItem(item: I_Item, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })
        console.log("STORAGEITEMPOST: ", JSON.stringify(item));

        let response = await this.httpClient.post(`https://localhost:7286/api/storageItem/`, JSON.stringify(item));
        return response.status;
    }

    async deleteItem(id: string, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        await this.httpClient.delete(`https://localhost:7286/api/storageItem/` + id);
    }
}