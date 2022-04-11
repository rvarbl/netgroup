import { IRouter } from "aurelia";
import { ILogin } from "../../domain/identity/ILogin";
import { AppState } from "../../state/AppState";

export class LoginView {
    loginData: ILogin | undefined;
    email: string | undefined;
    password: string | undefined
    
    constructor(private appState: AppState, @IRouter private router: IRouter) {

    }

    async loginForm() {
        console.log(this.email + " --- " + this.password);
        if (this.email !== undefined && this.password !== undefined) {

            this.appState.logIn(
                {
                    email: this.email,
                    password: this.password
                });
            await this.router.load(`/`);
        }

    }
}