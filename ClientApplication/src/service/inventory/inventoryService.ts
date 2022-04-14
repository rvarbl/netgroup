import { HttpClient } from "aurelia";
import { IUser } from "../../domain/identity/IUser";
import { IAttribute } from "../../domain/inventory/IAttribute";
import { IStorage } from "../../domain/inventory/IStorage";
import { I_Item } from "../../domain/inventory/I_Item";
import { I_ItemAttribute } from "../../domain/inventory/I_ItemAttribute";

export class InventoryService {
    url: string = "https://localhost:7286";
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
        let response = await this.httpClient.get(this.url + "/api/storage");
        let json = await response.json();
        let data: IStorage[] = json;

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

        let response = await this.httpClient.get(this.url + "/api/storage/" + id);
        let json = await response.json();
        let data: IStorage = json;
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

        await this.httpClient.delete(this.url + "/api/storage/" + id);
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
        return await this.httpClient.put(this.url + "/api/storage/", JSON.stringify(storage));
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
        return await this.httpClient.put(this.url + "/api/storageItem/" + item.id, JSON.stringify(item));
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

        let response = await this.httpClient.get(this.url + "/api/storageItem/" + id);
        let json = await response.json();
        let data: I_Item = json;
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

        await this.httpClient.get(this.url + "api/storage/" + id);
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

        let response = await this.httpClient.post(this.url + "/api/storageItem/", JSON.stringify(item));
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

        await this.httpClient.delete(this.url + "/api/storageItem/" + id);
    }
    async getAllAttributes(): Promise<IAttribute[]> {
        let response = await this.httpClient.get(this.url + "/api/itemAttribute");
        let json = await response.json();
        let data: IAttribute[] = json;

        return data;
    }

    async createItemAttribute(itemAttribute: I_ItemAttribute, user: IUser) {
        if (user !== undefined && itemAttribute !== undefined) {
            try {
                this.httpClient.configure(config => {
                    return config.withInterceptor({
                        request(request) {
                            request.headers.set('Authorization', 'Bearer ' + user?.token);
                            return request;
                        }
                    });
                })
                let response = await this.httpClient.post(this.url + "/api/attributeInItem/", JSON.stringify(itemAttribute));
                return response.status;
            }
            catch {
                //mingi error
                return;
            }
        }

    }
    async editAttribute(item: I_ItemAttribute, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })
        return await this.httpClient.put(this.url + "/api/attributeInItem/" + item.id, JSON.stringify(item));
    }

    async deleteAttribute(id: string, user: IUser) {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        await this.httpClient.delete(this.url + "/api/attributeInItem/" + id);
    }

    async getAttributeById(id: string, user: IUser): Promise<I_Item | undefined> {
        this.httpClient.configure(config => {
            return config.withInterceptor({
                request(request) {
                    request.headers.set('Authorization', 'Bearer ' + user?.token);
                    return request;
                }
            });
        })

        let response = await this.httpClient.get(this.url + "/api/attributeInItem/" + id);
        let json = await response.json();
        let data: I_ItemAttribute = json;

        return data;
    }
}