import { IUser } from "../../domain/identity/IUser";

export class IdentityRepository {

    logIn(email: string, pw: string): IUser | undefined {

        return;
    }

    logOut(user: IUser | undefined) {

    }

    refreshToken() {

    }

    register() {

    }
}