export interface IUser {
    email: string;
    
    role:string;

    firstName: string;
    lastName: string;

    token: string;
    refreshToken: string;
}