import { IUser } from "../domain/identity/IUser";

import { IStorage } from "../domain/inventory/IStorage";

export class StateManager {

    constructor(private storage: Storage) {
        console.log("storage constructed");
        
    }
    setUser(user: IUser) {
        console.log(user + " <-----------");
        if (user !== null) {
            let userString = JSON.stringify(user);
            console.log(userString + " <-----------");
            
            this.storage.setItem("user", userString)
        }
    }

    getUser(): IUser | undefined {
        let userString = this.storage.getItem("user");
        if (userString !== null) {
            return JSON.parse(userString);
        }
        console.log("Failed to get user: ", userString);
        return;

    }

}