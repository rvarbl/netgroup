import { Params } from "aurelia";

export class StorageEditView{
    id?:string;
    async load(params: Params){
        this.id=params["id"];
        console.log(params);
    }
}