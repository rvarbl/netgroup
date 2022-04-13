import { Params } from "aurelia";
import { I_ItemAttribute } from "../../../../domain/inventory/I_ItemAttribute";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class ItemViewEdit {
    id?: string;
    item?: I_Item;
    attributes?: I_ItemAttribute

    itemName?: string;
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
    async editItem() {
        

        if (this.item !== undefined) {
            let newItem = {
                id: this.item.id,
                itemName: this.item.itemName,
                storageId: this.item.storageId,
            }
            if (this.itemName !== undefined) {
                this.item.itemName = this.itemName;
            }
            console.log("edit1", newItem);
            await this.appState.editItem(newItem);
        }


    }
}