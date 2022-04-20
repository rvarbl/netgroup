import { ILogin } from "../domain/identity/ILogin";
import { IUser } from "../domain/identity/IUser";
import { IdentityService } from "../service/identity/identityService";
import { IRegister } from "../domain/identity/IRegister";
import { InventoryService } from "../service/inventory/inventoryService";
import { IStorage } from "../domain/inventory/IStorage";
import { HttpClient, IRouter } from "aurelia";
import { I_Item } from "../domain/inventory/I_Item";
import { IAttribute } from "../domain/inventory/IAttribute";
import { I_ItemAttribute } from "../domain/inventory/I_ItemAttribute";
import { ImageService } from "../service/files/imageService";

export class AppState {
    imageService: ImageService;
    inventoryService: InventoryService;
    identityService: IdentityService;
    user: IUser | undefined;

    constructor(private httpClient: HttpClient, @IRouter private router: IRouter) {
        this.identityService = new IdentityService();
        this.inventoryService = new InventoryService();
        this.imageService = new ImageService();
        if (this.user === undefined) {
            this.getLocalStorage();
        }

    }

    //storage
    setLocalStorage() {
        if (this.user !== undefined) {
            let userString = JSON.stringify(this.user)
            localStorage.setItem("user", userString);
        }

    }
    getLocalStorage() {
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

            this.setLocalStorage();
            return await this.router.load(`/`);
        }
        return await this.router.load(`/login`);
    }

    async register(register: IRegister) {
        let user = await this.identityService.register(register);
        if (user !== undefined) {
            this.user = user;
            this.user.roles = ["user"]; //Change this!
            this.setLocalStorage();
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
    //inventory
    async createStorage(storage: IStorage): Promise<string | undefined> {
        if (this.user !== undefined) {
            try {
                await this.inventoryService.createStorage(storage, this.user);
                return;
            }
            catch {
                //mingi error
                return;
            }
        }
        console.log("Undefined user!");
        return undefined;
    }

    async createItem(item: I_Item): Promise<string | undefined> {
        if (this.user !== undefined) {
            try {
                await this.inventoryService.createItem(item, this.user);
                return;
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
                return await this.inventoryService.editStorage(storage, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }

    }

    async editItem(item: I_Item) {
        if (item !== undefined && this.user !== undefined) {
            try {
                return await this.inventoryService.editItem(item, this.user);
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
                return;
            }
            catch {
                //mingi error
                return;
            }
        }
        console.log("Undefined user!");
        return undefined;
    }
    async getAllAttributes(): Promise<IAttribute[] | undefined> {
        try {
            return await this.inventoryService.getAllAttributes();
        }
        catch {
            //mingi error
            return;
        }
    }
    async createItemAttribute(itemAttribute: I_ItemAttribute) {

        if (this.user !== undefined && itemAttribute !== undefined) {
            try {
                return await this.inventoryService.createItemAttribute(itemAttribute, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }

    }
    async editAttribute(attribute: I_ItemAttribute) {
        console.log("EDITING2: ", attribute);
        if (attribute !== undefined && this.user !== undefined) {
            try {
                return await this.inventoryService.editAttribute(attribute, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
    }

    async deleteAttribute(id: string) {
        if (this.user !== undefined) {
            try {
                this.inventoryService.deleteAttribute(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
        console.log("Undefined user!");
        return undefined;
    }

    async getAttributeById(id: string): Promise<I_Item | undefined> {
        if (id !== undefined && this.user !== undefined) {
            try {
                return await this.inventoryService.getAttributeById(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
    }

    async getImage(id: string) {
        if (id !== undefined && this.user !== undefined) {
            try {               
                return await this.imageService.getImage(id, this.user);
            }
            catch {
                //mingi error
                return;
            }
        }
    }


}