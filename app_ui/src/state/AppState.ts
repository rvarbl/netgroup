import { IUser } from "../domain/identity/IUser";
import { IdentityRepository } from "../repositories/identity/identityRepository";

export class AppState {
    identityRepo: IdentityRepository;
    user:IUser;
    constructor(){
        

        this.identityRepo = new IdentityRepository();
    }

    logOut(){
        this.identityRepo.logOut(this.user);
        this.user = undefined;
    }
}