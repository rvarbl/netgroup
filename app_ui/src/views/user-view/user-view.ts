import { IRouter } from "aurelia";
import { IStorage } from "../../domain/inventory/IStorage";
import { AppState } from "../../state/AppState";

export class UserView {
    storages: IStorage[] | undefined;
    constructor(private appState: AppState, @IRouter private router: IRouter) {
        this.storages = [
            { ChildStorage: undefined, ParentStorageId: "123", StorageName: "name" }
        ]
    }

    getAllStorages() {
        this.storages = this.appState.getAllStorages()
        console.log("GETALL + " + this.storages);
    }
    async detailView(id: string) {
        await this.router.load(`./storage-detail-view`);
    }
}