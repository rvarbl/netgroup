export class IStorage {
    ParentStorageId: string | undefined;
    ChildStorage: IStorage[] | undefined;
    StorageName: string | undefined
}