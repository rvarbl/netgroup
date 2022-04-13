import { IRouter, Params } from "aurelia";
import { IStorage } from "../../../../domain/inventory/IStorage";
import { AppState } from "../../../../state/AppState";

export class StorageEditView {
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

            console.log("EDITING: ", newStorage);
            await this.appState.editStorage(newStorage);
            //return await this.router.load(`/storage/details/` + this.storage.id)
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
    }



}