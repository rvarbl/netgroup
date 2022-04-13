export interface IUser {
    email: string;
    
    roles:string[];

    firstName: string;
    lastName: string;

    token: string;
    refreshToken: string;
}