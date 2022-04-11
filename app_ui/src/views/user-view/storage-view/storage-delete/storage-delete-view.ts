import { Params } from "aurelia";

export class StorageDeleteView{
    id?:string;
    async load(params: Params){
        this.id=params["id"];
        console.log(params);
    }
}