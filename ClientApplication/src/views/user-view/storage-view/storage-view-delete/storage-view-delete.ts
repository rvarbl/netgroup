import { IRouter, Params } from "aurelia";
import { IStorage } from "../../../../domain/inventory/IStorage";
import { AppState } from "../../../../state/AppState";

export class StorageViewDelete {
    id?: string;
    storage?: IStorage;
    constructor(private appState: AppState, @IRouter private router: IRouter){

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
    async delete(){
        if (this.id !== undefined) {
            this.storage = await this.appState.deleteStorage(this.id);
            console.log("DELETESTORAGE: ", this.storage);
            await this.router.load(`/storage`);
        }
    }
}