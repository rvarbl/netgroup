import { AppState } from "../../../state/AppState";

export class PageHeader {
    constructor(private appState: AppState) {

    }
    isRoleUser(): boolean {
        if (this.appState.user) {
            return this.appState.user.role === "user";
        }
        return false;
    }
    isRoleAdmin(): boolean {
        if (this.appState.user) {
            return this.appState.user.role === "admin";
        }
        return false;
    }
}