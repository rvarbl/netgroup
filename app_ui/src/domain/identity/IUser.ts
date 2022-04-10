export interface IUser {
    email: string;
    
    role:string;

    firstName: string;
    lastName: string;

    jwt: string;
    refreshToken: string;
}