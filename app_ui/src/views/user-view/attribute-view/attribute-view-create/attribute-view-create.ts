import { IRouter, Params } from "aurelia";
import { IAttribute } from "../../../../domain/inventory/IAttribute";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { I_ItemAttribute } from "../../../../domain/inventory/I_ItemAttribute";
import { AppState } from "../../../../state/AppState";

export class AttributeViewCreate {
    itemId?: string;
    attributes?: IAttribute[];
    selectedAttributeId?: string;
    attributeValue?: string;

    constructor(private appState: AppState, @IRouter private router: IRouter) {
    }

    async load(params: Params) {
        this.itemId = params["id"];
        this.getAllAtributes();
    }

    async getAllAtributes() {
        if (this.itemId !== undefined) {
            this.attributes = await this.appState.getAllAttributes();
        }
    }

    async createNewItemAttribute() {
        
        
        if (this.itemId !== undefined && this.selectedAttributeId !== undefined && this.attributeValue !== undefined) {
            console.log("123123123", this.itemId, this.selectedAttributeId, this.attributeValue );
            let newItemAttribute: I_ItemAttribute = {
                itemId: this.itemId,
                attributeId: this.selectedAttributeId,
                attributeValue: this.attributeValue
            }
            await this.appState.createItemAttribute(newItemAttribute);
            return await this.router.load(`/item/details/` + this.itemId)
        }
    }
}