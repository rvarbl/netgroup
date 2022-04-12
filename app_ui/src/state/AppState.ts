import { MainView } from "../views/main-view/main-view";
import { ILogin } from "../domain/identity/ILogin";
import { IUser } from "../domain/identity/IUser";
import { IdentityService } from "../service/identity/identityService";
import { IRegister } from "../domain/identity/IRegister";
import { InventoryService } from "../service/inventory/inventoryService";
import { IStorage } from "../domain/inventory/IStorage";
import { HttpClient } from "aurelia";
import { I_Item } from "../domain/inventory/I_Item";

export class AppState {
    inventoryService: InventoryService;
    identityService: IdentityService;
    user: IUser | undefined;

    constructor(private httpClient: HttpClient) {
        this.identityService = new IdentityService();
        this.inventoryService = new InventoryService();
        this.user = {
            email: "suAdmin@test.ee",
            role: "user",
            firstName: "admin",
            lastName: "123",
            jwt: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjliY2NjMjVlLWJkZmMtNGUzNy1hMWMwLTdjNjk5MDkyZDUwZSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdUFkbWluQHRlc3QuZWUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzdUFkbWluQHRlc3QuZWUiLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6IjVGUVg2VkQzQVEzSUFJVldHUDRITVozWjJQM0JZTFUyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJleHAiOjE2NDk3NjAzNDIsImlzcyI6ImludmVudG9yeWFwcC5ydmFyYmwuY29tIiwiYXVkIjoiaW52ZW50b3J5YXBwLnJ2YXJibC5jb20ifQ.YwIlbJPIvDKHd0rjyCs_h0ah5ITNJCWmmTWGVIcwndM",
            refreshToken: "e3754e58-2f7b-4ccb-b559-7c0b24d64d86"
        }
    }

    //identity
    async logIn(login: ILogin) {
        let userPromise = this.identityService?.logIn(login);
        if (userPromise !== undefined) {
            userPromise.then(x => this.user = x);
            console.log("user: " + this.user?.firstName)

            return MainView;
        }

    }

    register(register: IRegister) {
        let userPromise = this.identityService.register(register);
        if (userPromise !== undefined) {
            userPromise.then(x => this.user = x);
            console.log("user: " + this.user?.firstName)
            return MainView;
        }
    }

    logOut() {
        if (this.identityService !== undefined && this.user !== undefined) {
            this.identityService.logOut(this.user);
            this.user = undefined;
        }
    }

    //inventory
    async getAllStorages(): Promise<IStorage[] | undefined> {
        if (this.user !== undefined) {
            try {
                return await this.inventoryService.getAllStorages(this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
        console.log("Undefined user!");
        return undefined;
    }

    async getStorageById(id: string): Promise<IStorage | undefined> {
        if (this.user !== undefined) {
            try {
                return await this.inventoryService.getStorageById(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
        console.log("Undefined user!");
        return undefined;
    }

    async deleteStorage(id: string) {
        if (this.user !== undefined) {
            try {
                this.inventoryService.deleteStorage(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
        console.log("Undefined user!");
        return undefined;
    }

    async editStorage(storage: IStorage) {
        if (storage !== undefined && this.user !== undefined) {
            try {
                this.inventoryService.editStorage(storage, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }

    }

    async getItemById(id: string): Promise<I_Item | undefined> {
        if (id !== undefined && this.user !== undefined) {
            try {
                return await this.inventoryService.getItemById(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
    }

    async getItemsByStorageId(id: string): Promise<I_Item[] | undefined> {
        if (id !== undefined && this.user !== undefined) {
            try {
                this.inventoryService.getItemsByStorageId(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
    }
}