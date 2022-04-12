import { Params } from "aurelia";
import { IStorage } from "../../../../domain/inventory/IStorage";
import { AppState } from "../../../../state/AppState";

export class StorageEditView {
    id?: string;
    storage?: IStorage;
    storages?: IStorage[];

    x?: string;

    constructor(private appState: AppState) {
        this.getAllStorages();
    }

    async load(params: Params) {
        this.id = params["id"];
        await this.getStorageById();
        console.log("Load");
    }

    async editStorage() {
        if (this.storage !== undefined) {
            await this.appState.editStorage(this.storage);
        }
        console.log("FAILURE: ", this.storage);
    }

    private async getStorageById() {
        if (this.id !== undefined) {
            this.storage = await this.appState.getStorageById(this.id);
            console.log("GETSTORAGEID: ", this.storage);
        }
    }

    async getAllStorages() {
        this.storages = await this.appState.getAllStorages();

    }



}