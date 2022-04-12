import { I_ItemAttribute } from "./I_ItemAttribute";


export interface I_Item {
    Id?: string;
    ItemName?: string;
    StorageId?: string;
    ItemAttributes?: I_ItemAttribute[];
}