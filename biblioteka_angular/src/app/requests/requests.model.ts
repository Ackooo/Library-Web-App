export interface requestsCreateDTO{
    bookId:number;
}

export interface requestsDTO{
    id:number;
    bookId:number;
    userId:string;
    dateOfReturn:string;
    userName:string;
    bookTitle:string;
    userIssuedNumber:number;
    state:number;
}