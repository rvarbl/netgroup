import { IRouter } from "aurelia";
import { IRegister } from "../../domain/identity/IRegister";
import { AppState } from "../../state/AppState";

export class RegisterView {
    email: string | undefined;
    password: string | undefined;
    firstName: string | undefined;
    lastName: string | undefined;

    constructor(private appState: AppState, @IRouter private router: IRouter) {

    }
    async registerForm() {
        console.log(this.email);
        
        if (this.email !== undefined && this.password !== undefined && this.firstName !== undefined && this.lastName !== undefined) {
            console.log("HERE!");
            let registerDto: IRegister = {
                email: this.email,
                password: this.password,
                firstName: this.firstName,
                lastName: this.lastName
            }
            this.appState.register(registerDto);
            await this.router.load(`/`);
        }

    }
}