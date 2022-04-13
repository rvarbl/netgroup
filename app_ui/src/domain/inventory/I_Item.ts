import { I_ItemAttribute } from "./I_ItemAttribute";


export interface I_Item {
    id?: string;
    itemName?: string;
    storageId?: string;
    itemAttributes?: I_ItemAttribute[];
}