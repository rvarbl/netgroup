import { IRouter, Params } from "aurelia";
import { I_ItemAttribute } from "../../../../domain/inventory/I_ItemAttribute";
import { AppState } from "../../../../state/AppState";

export class AttributeViewDelete{
    id?: string;
    attribute?: I_ItemAttribute;
    constructor(private appState: AppState, @IRouter private router: IRouter) {

    }
    async load(params: Params) {
        this.id = params["id"];
        this.getAttributeById();
        console.log(params);
    }

    async getAttributeById() {
        if (this.id !== undefined) {
            //this.attribute = await this.appState.getAttributeById(this.id);
            console.log("GETSTORAGEITEM: ", this.attribute);
        }
    }

    async delete() {
        if (this.id !== undefined) {
            //this.attribute = await this.appState.deleteAttribute(this.id);
            console.log("DELETEITEM: ", this.attribute);
            await this.router.load(`/item`);
        }
    }
}