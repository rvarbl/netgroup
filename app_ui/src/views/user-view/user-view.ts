import { IStorage } from "../../domain/inventory/IStorage";
import { AppState } from "../../state/AppState";

export class UserView {
    storages: IStorage[] | undefined;
    constructor(private appState: AppState) {
        this.storages = [
             {ChildStorage:undefined, ParentStorageId:"123", StorageName: "name"}
        ]
    }

    private getAllStorages() {
        this.storages = this.appState.getAllStorages()
        console.log("GETALL + " + this.storages);
    }
}