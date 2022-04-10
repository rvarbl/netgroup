import { IUser } from "../domain/identity/IUser";
import { IdentityRepository } from "../repositories/identity/identityRepository";

export class AppState {
    identityRepo: IdentityRepository;
    user:IUser | undefined;
    constructor(){
        this.user = {
            email: "string",
            
            role:"user",
        
            firstName: "string",
            lastName: "string",
        
            jwt: "string",
            refreshToken: "string"
        }

        this.identityRepo = new IdentityRepository();
    }

    logOut(){
        this.identityRepo.logOut(this.user);
        this.user = undefined;
    }
}