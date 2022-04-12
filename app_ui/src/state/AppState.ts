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
        console.log("USERRUSEUTSUTE", this.user);

    }

    //storage
    setStorage() {
        if (this.user !== undefined) {
            let x = JSON.stringify(this.user)
            localStorage.setItem("user", x);
        }

    }
    getStorage() {
        let z = localStorage.getItem("user");
        if (z !== null && z.length > 1) {
            let y: IUser = JSON.parse(z)
            if (y !== undefined) {
                this.user = y;
            }
        }
    }

    //identity
    async logIn(login: ILogin) {
        let user = await this.identityService?.logIn(login);
        if (user !== undefined) {
            this.user = user;
            this.user.role = "user" //Change this!
            this.setStorage();
            await this.router.load(`/`);
        }

    }

    async register(register: IRegister) {
        let user = await this.identityService.register(register);
        if (user !== undefined) {
            console.log("user: " + this.user?.firstName)
            this.user = user;
            this.user.role = "user" //Change this!
            this.setStorage();
            return MainView;
        }
    }

    logOut() {
        if (this.identityService !== undefined && this.user !== undefined) {
            this.identityService.logOut(this.user);
            this.user = undefined;
            localStorage.setItem("user", "")
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