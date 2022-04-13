import { Params } from "aurelia";
import { I_ItemAttribute } from "../../../../domain/inventory/I_ItemAttribute";
import { I_Item } from "../../../../domain/inventory/I_Item";
import { AppState } from "../../../../state/AppState";

export class ItemViewEdit {
    id?: string;
    item?: I_Item;
    attributes?: I_ItemAttribute

    itemName?: string;
    attributeId?: string;
    attributeValue?: string;

    images: I_ItemAttribute[] = []
    otherAttributes: I_ItemAttribute[] = []

    constructor(private appState: AppState) {
    }

    async load(params: Params) {
        this.id = params["id"];
        await this.getItemById();

       
    }

    async getItemById() {
        if (this.id !== undefined) {
            this.item = await this.appState.getItemById(this.id);
            console.log("GETSTORAGEITEM: ", this.item);

             this.item?.itemAttributes?.forEach(x=>{
                if(x.attributeName === "Image"){
                    //getImage
                }
                else{
                    console.log(x, "not_image");
                    this.otherAttributes.push(x) 
                }
            })
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

    async handleAttributeEdit() {
        if (this.item !== undefined) {
            console.log("handleAttributeEdit", this.attributeId, this.attributeValue, this.item);

            this.item.itemAttributes?.forEach((
                attribute: I_ItemAttribute) => {
                this.appState.editAttribute(attribute);
            }
            )
        }
    }
    



}
