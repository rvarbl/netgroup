import { Params } from "aurelia";
import { IStorage } from "../../../../domain/inventory/IStorage";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class StorageDetailView {
    id?: string;
    storage?: IStorage;
    items?:I_Item[];

    constructor(private appState: AppState) {

    }

    async load(params: Params) {
        this.id = params["id"];
        this.getStorageById();
        console.log(params);
    }

    private async getStorageById() {
        if (this.id !== undefined) {
            this.storage = await this.appState.getStorageById(this.id);
            console.log("GETSTORAGEID: ", this.storage);
        }
    }


    private async getItemsByStorageId() {
        if (this.id !== undefined) {
            this.items = await this.appState.getItemsByStorageId(this.id);
            console.log("getItemsByStorageId: ", this.storage);
                
        }
    }
}