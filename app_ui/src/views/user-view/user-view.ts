import { IRouter } from "aurelia";
import { IStorage } from "../../domain/inventory/IStorage";
import { AppState } from "../../state/AppState";

export class UserView {
    storages?: IStorage[];
    constructor(private appState: AppState, @IRouter private router: IRouter) {
        this.storages = [
            { Id: "123",ChildStorage: undefined, ParentStorageId: "123", StorageName: "name" }
        ]
    }

    async getAllStorages() {
        console.log(this.appState.user?.email);
        
        this.storages = await this.appState.getAllStorages()
        console.log("GETALL + " + this.storages);
    }
    
}