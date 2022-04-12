import { Params } from "aurelia";
import { I_ItemAttribute } from "../../../../domain/inventory/I_ItemAttribute";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class ItemViewEdit {
    id?: string;
    item?: I_Item;
    attributes?:I_ItemAttribute

    constructor(private appState: AppState) {
    }

    async load(params: Params) {
        this.id = params["id"];
        await this.getItemById();
        console.log(params);
    }

    async getItemById() {
        if (this.id !== undefined) {
            this.item = await this.appState.getItemById(this.id);
            console.log("GETSTORAGEITEM: ", this.item);
        }

    }
}