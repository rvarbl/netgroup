import { IRouter } from "aurelia";
import { IStorage } from "../../../../domain/inventory/IStorage";
import { AppState } from "../../../../state/AppState";

export class StorageCreateView {
    storageName?: string;
    parentStorageId?: string;
    storages?: IStorage[];

    constructor(private appState: AppState, @IRouter private router: IRouter) {
        this.getAllStorages();
    }

    async createStorage() {
        let storage: IStorage = {
            parentStorageId: this.parentStorageId,
            storageName: this.storageName
        }

        await this.appState.createStorage(storage);

        console.log("create: ", storage);
        await this.router.load(`/storage`);
    }

    private async getAllStorages() {

        this.storages = await this.appState.getAllStorages();
        console.log("GETSTORAGEID: ", this.storages);
    }

}