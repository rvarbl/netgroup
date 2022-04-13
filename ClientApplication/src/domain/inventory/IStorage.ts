import { I_Item } from "./I_Item";

export class IStorage {
    id?: string;
    parentStorageId?: string;
    childStorage?: IStorage[];
    storageName?: string;
    storageItems?:I_Item[];
}