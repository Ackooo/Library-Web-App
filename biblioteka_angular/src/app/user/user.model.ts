export interface userLogin{
    email:string;
    password:string;
}

export interface userRegister{
    email:string;
    password:string;
    type:number;
}
export interface authenticationDTO{
    token:string;
}