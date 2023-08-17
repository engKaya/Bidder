export class ResponseMessage<T> {
    constructor() {
        this.Errors = [];
        this.StatusCode = 200;
        this.Message = '';
        this.Data = new Object() as T;
    }

    Errors: string[];
    StatusCode: number;
    Message: string;
    Data: T;
}