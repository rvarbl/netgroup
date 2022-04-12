import { I_Item } from "./I_Item";

export class IStorage {
    Id?: string;
    ParentStorageId?: string;
    ChildStorage?: IStorage[];
    StorageName?: string;
    StorageItems?:I_Item[];
}