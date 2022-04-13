import { IRouter, Params } from "aurelia";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class ItemViewCreate {
    storageId?: string;
    itemName?: string;

    constructor(private appState: AppState, @IRouter private router: IRouter) {
    }

    async load(params: Params) {
        this.storageId = params["id"];
    }


    async createItem() {
        if (this.itemName !== undefined && this.storageId !== undefined) {
            let item: I_Item = { storageId: this.storageId, itemName: this.itemName };
            this.appState.createItem(item);
            return await this.router.load(`/storage/details/` + this.storageId)
        }

        //loo uus item
        //salvesta
    }
}