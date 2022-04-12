import { Params } from "aurelia";
import { IAttribute } from "../../../../domain/inventory/IAttribute";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class AttributeViewCreate{
    id?: string;
    item?: I_Item;
    attributes?:IAttribute[];
    
    constructor(private appState: AppState) {
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
}