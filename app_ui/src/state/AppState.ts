import { MainView } from "../views/main-view/main-view";
import { ILogin } from "../domain/identity/ILogin";
import { IUser } from "../domain/identity/IUser";
import { IdentityService } from "../service/identity/identityService";
import { IRegister } from "../domain/identity/IRegister";
import { InventoryService } from "../service/inventory/inventoryService";
import { IStorage } from "../domain/inventory/IStorage";
import { HttpClient, IRouter } from "aurelia";
import { I_Item } from "../domain/inventory/I_Item";

export class AppState {
    inventoryService: InventoryService;
    identityService: IdentityService;
    user: IUser | undefined;

    constructor(private httpClient: HttpClient, @IRouter private router: IRouter) {
        this.identityService = new IdentityService();
        this.inventoryService = new InventoryService();
        if (this.user === undefined) {
            this.getStorage();
        }

    }

    //storage
    setStorage() {
        if (this.user !== undefined) {
            let userString = JSON.stringify(this.user)
            localStorage.setItem("user", userString);
        }

    }
    getStorage() {
        let userString = localStorage.getItem("user");
        if (userString !== null) {
            if (userString.length > 1) {
                let user: IUser = JSON.parse(userString)
                if (user !== undefined) {
                    this.user = user;
                    console.log("AppState: ", userString)
                    return;
                }
            }
        }
        console.log("AppState: Failed to get user!")
    }

    //identity
    async logIn(login: ILogin) {
        let user = await this.identityService?.logIn(login);
        if (user !== undefined) {
            this.user = user;
            this.user.role = "user" //Change this!
            this.setStorage();
            return await this.router.load(`/`);
        }
        return await this.router.load(`/login`);
    }

    async register(register: IRegister) {
        let user = await this.identityService.register(register);
        if (user !== undefined) {
            console.log("user: " + this.user?.firstName)
            this.user = user;
            this.user.role = "user" //Change this!
            this.setStorage();
            return await this.router.load(`/`);
        }
    }

    async logOut() {
        if (this.identityService !== undefined && this.user !== undefined) {
            this.identityService.logOut(this.user);
            this.user = undefined;
            localStorage.setItem("user", "")
        }
        return await this.router.load(`/`);
    }

    //inventory
    async getAllStorages(): Promise<IStorage[] | undefined> {
        if (this.user !== undefined) {
            try {
                console.log("getAllStorages ", this.user);
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
    async deleteItem(id: string) {
        if (this.user !== undefined) {
            try {
                this.inventoryService.deleteItem(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
        console.log("Undefined user!");
        return undefined;
    }
}