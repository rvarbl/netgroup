import { IStorage } from "../../../../domain/inventory/IStorage";
import { AppState } from "../../../../state/AppState";

export class StorageCreateView {
    storageName?: string;
    parentStorageId?: string;
    storages?: IStorage[];

    constructor(private appState: AppState) {
        this.getAllStorages();
    }

    async createStorage() {
        let storage: IStorage = {
            ParentStorageId: this.parentStorageId,
            StorageName: this.storageName
        }

        //await this.appState.createStorage(storage);

        console.log("create: ", storage);
    }

    private async getAllStorages() {

        this.storages = await this.appState.getAllStorages();
        console.log("GETSTORAGEID: ", this.storages);
    }

}