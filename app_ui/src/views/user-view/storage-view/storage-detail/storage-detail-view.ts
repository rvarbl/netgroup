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
    }

    private async getStorageById() {
        if (this.id !== undefined) {
            this.storage = await this.appState.getStorageById(this.id);
        }
    }
}