import { Params } from "aurelia";
import { IStorage } from "../../../../domain/inventory/IStorage";
import { AppState } from "../../../../state/AppState";

export class ItemViewCreate {
    id?: string;
    storage?: IStorage;

    itemName?: string;

    constructor(private appState: AppState) {
    }

    async load(params: Params) {
        this.id = params["id"];
        this.getStorageById();
        console.log(params);
    }

    async getStorageById() {
        if (this.id !== undefined) {
            this.storage = await this.appState.getStorageById(this.id);
            console.log("GETSTORAGE: ", this.storage);
        }

    }
    
    async createAddNewItemToStorage(){
        //loo uus item
        //salvesta
    }
}