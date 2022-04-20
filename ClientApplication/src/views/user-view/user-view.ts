import { IRouter } from "aurelia";
import { IStorage } from "../../domain/inventory/IStorage";
import { AppState } from "../../state/AppState";

export class UserView {
    storages?: IStorage[];
    constructor(private appState: AppState, @IRouter private router: IRouter) {
        this.getAllStorages();
    }

    async getAllStorages() {
        this.storages = await this.appState.getAllStorages()
        this.storages = this.storages?.filter(x => x.parentStorageId == null)
    }

}