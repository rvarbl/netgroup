import { IAttribute } from "./IAttribute"

export interface I_Item {
    Id: string;
    ItemName: string;
    StorageId?: string;
    ItemAttributes?: IAttribute[];
}