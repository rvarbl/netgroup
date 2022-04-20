import { IRouter, IRouteViewModel, Params } from "aurelia";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class ItemViewDelete implements IRouteViewModel{
    id?: string;
    item?: I_Item;
    constructor(private appState: AppState, @IRouter private router: IRouter) {

    }
    async load(params: Params) {
        this.id = params["id"];
        this.getItemById();
    }

    async getItemById() {
        if (this.id !== undefined) {
            this.item = await this.appState.getItemById(this.id);
        }
    }

    async delete() {
        if (this.id !== undefined) {
            let storageId = this.item?.storageId;
            this.item = await this.appState.deleteItem(this.id);
            return await this.router.load(`/storage/details/` + storageId);
        }
    }
}