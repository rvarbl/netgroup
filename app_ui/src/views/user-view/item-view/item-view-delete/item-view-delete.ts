import { IRouter, Params } from "aurelia";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class ItemViewDelete {
    id?: string;
    item?: I_Item;
    constructor(private appState: AppState, @IRouter private router: IRouter) {

    }
    async load(params: Params) {
        this.id = params["id"];
        this.getItemById();
        console.log(params);
    }

    async getItemById() {
        if (this.id !== undefined) {
            this.item = await this.appState.getItemById(this.id);
            console.log("GETSTORAGEITEM: ", this.item);
        }
    }

    async delete() {
        if (this.id !== undefined) {
            this.item = await this.appState.deleteItem(this.id);
            console.log("DELETEITEM: ", this.item);
            await this.router.load(`/item`);
        }
    }
}