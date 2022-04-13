import { IRouter } from "aurelia";
import { IStorage } from "../../domain/inventory/IStorage";
import { AppState } from "../../state/AppState";

export class UserView {
    storages?: IStorage[];
    constructor(private appState: AppState, @IRouter private router: IRouter) {
        this.getAllStorages();
        this.filterStorages();
    }

    async getAllStorages() {
        console.log(this.appState.user?.email);
        this.storages = await this.appState.getAllStorages()
        console.log("GETALL + ", this.storages, this.appState.user?.email);
    }
    filterStorages(){

    }
    
}