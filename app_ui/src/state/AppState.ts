import { MainView } from "../views/main-view/main-view";
import { ILogin } from "../domain/identity/ILogin";
import { IUser } from "../domain/identity/IUser";
import { IdentityRepository } from "../repositories/identity/identityRepository";
import { IRegister } from "../domain/identity/IRegister";

export class AppState {

    identityRepo: IdentityRepository;
    user: IUser | undefined;
    constructor() {
        this.identityRepo = new IdentityRepository();
    }

    async logIn(login: ILogin) {
        let userPromise = this.identityRepo?.logIn(login);
        if (userPromise !== undefined) {
            userPromise.then(x => this.user = x);
            console.log("user: " + this.user?.firstName)
            return MainView;
        }

    }

    register(register: IRegister) {
        let userPromise = this.identityRepo.register(register);
        if (userPromise !== undefined) {
            userPromise.then(x => this.user = x);
            console.log("user: " + this.user?.firstName)
            return MainView;
        }
    }

    logOut() {
        if (this.identityRepo !== undefined && this.user !== undefined) {
            this.identityRepo.logOut(this.user);
            this.user = undefined;
        }
    }

}