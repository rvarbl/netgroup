export class IStorage {
    Id?: string;
    ParentStorageId?: string;
    ChildStorage?: IStorage[];
    StorageName?: string;
}