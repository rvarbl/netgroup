import { Params } from "aurelia";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class ItemViewDetail {
    id?: string;
    item?: I_Item;

    constructor(private appState: AppState) {
    }

    async load(params: Params) {
        this.id = params["id"];
        this.getItemById();
        console.log("params itemviewdeatail _> ", this.item);
    }

    async getItemById() {
        if (this.id !== undefined) {
            this.item = await this.appState.getItemById(this.id);
            console.log("GETSTORAGEITEM: ", this.item);
        }

    }
}