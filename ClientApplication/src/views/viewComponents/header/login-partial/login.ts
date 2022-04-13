import { AppState } from "../../../../state/AppState";

export class Login{
        constructor(private appState: AppState){
        console.log(appState.user);
    }

    logout(){
        this.appState.logOut();
    }
}