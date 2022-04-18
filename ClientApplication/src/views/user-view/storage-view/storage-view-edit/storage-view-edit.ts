import { IRouter, Params } from "aurelia";
import { IStorage } from "../../../../domain/inventory/IStorage";
import { AppState } from "../../../../state/AppState";

export class StorageViewEdit {
    id?: string;
    storage?: IStorage;
    storages?: IStorage[];

    storageName?: string;
    parentId?: string;

    constructor(private appState: AppState, @IRouter private router: IRouter) {
        this.getAllStorages();
    }

    async load(params: Params) {
        this.id = params["id"];
        this.getStorageById();

    }

    async editStorage() {
        console.log("FAILURE: ", this.storageName, this.parentId);
        if (this.storage !== undefined) {
            let newStorage: IStorage = {
                id: this.storage.id,
                parentStorageId: this.storage.parentStorageId,
                storageName: this.storage.storageName
            }
            if (this.parentId !== undefined) {
                newStorage.parentStorageId = this.parentId;
            }
            if (this.storageName !== undefined) {
                newStorage.storageName == this.storageName;
            }
            await this.appState.editStorage(newStorage);
        }
        console.log("FAILURE: ", this.storage);
    }

    private async getStorageById() {
        if (this.id !== undefined) {
            this.storage = await this.appState.getStorageById(this.id);
        }
    }

    async getAllStorages() {
        this.storages = await this.appState.getAllStorages();
        this.storages = this.storages?.filter(x=> x.id != this.id)
    }



}