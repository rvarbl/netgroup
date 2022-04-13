import { Params } from "aurelia";
import { I_Item } from "../../../domain/inventory/I_Item";
import { AppState } from "../../../state/AppState";

export class ImageViewAdd{
    itemId?: string;
    item?: I_Item;
    
    constructor(private appState: AppState) {
    }
    async load(params: Params) {
        this.itemId = params["id"];
        await this.getItemById();
        console.log(this.item);
    }
    async getItemById() {
        if (this.itemId !== undefined) {
            this.item = await this.appState.getItemById(this.itemId);
            console.log("GETSTORAGEITEM: ", this.item);
        }
    }
}