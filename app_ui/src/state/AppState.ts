import { MainView } from "../views/main-view/main-view";
import { ILogin } from "../domain/identity/ILogin";
import { IUser } from "../domain/identity/IUser";
import { IdentityRepository } from "../repositories/identity/identityRepository";
import { IRegister } from "../domain/identity/IRegister";
import { InventoryRepository } from "../repositories/inventory/inventoryRepository";
import { IStorage } from "../domain/inventory/IStorage";

export class AppState {
    inventoryRepo: InventoryRepository;
    identityRepo: IdentityRepository;
    user: IUser | undefined;

    constructor() {
        this.identityRepo = new IdentityRepository();
        this.inventoryRepo = new InventoryRepository();
        this.user = {
            email: "suAdmin@test.ee",

            role: "user",

            firstName: "admin",
            lastName: "123",

            jwt: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjliY2NjMjVlLWJkZmMtNGUzNy1hMWMwLTdjNjk5MDkyZDUwZSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzdUFkbWluQHRlc3QuZWUiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJzdUFkbWluQHRlc3QuZWUiLCJBc3BOZXQuSWRlbnRpdHkuU2VjdXJpdHlTdGFtcCI6IjVGUVg2VkQzQVEzSUFJVldHUDRITVozWjJQM0JZTFUyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiYWRtaW4iLCJleHAiOjE2NDk2ODM5NzIsImlzcyI6ImludmVudG9yeWFwcC5ydmFyYmwuY29tIiwiYXVkIjoiaW52ZW50b3J5YXBwLnJ2YXJibC5jb20ifQ.FIafdeifPRwxYTff6TiC02yaQsDbTDXTYiJxcnmM_LE",
            refreshToken: "string"
        }
    }

    async logIn(login: ILogin) {
        let userPromise = this.identityRepo?.logIn(login);
        if (userPromise !== undefined) {
            userPromise.then(x => this.user = x);
            console.log("user: " + this.user?.firstName)
            return MainView;
        }

    }

    register(register: IRegister) {
        let userPromise = this.identityRepo.register(register);
        if (userPromise !== undefined) {
            userPromise.then(x => this.user = x);
            console.log("user: " + this.user?.firstName)
            return MainView;
        }
    }

    logOut() {
        if (this.identityRepo !== undefined && this.user !== undefined) {
            this.identityRepo.logOut(this.user);
            this.user = undefined;
        }
    }
    getAllStorages(): IStorage[] | undefined {
        if (this.user !== undefined) {
            this.inventoryRepo.getAllStorages(this.user).then(x => { return x });
        }
        return undefined;
    }

}