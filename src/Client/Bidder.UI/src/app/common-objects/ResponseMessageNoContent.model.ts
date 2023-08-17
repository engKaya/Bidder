export class ResponseMessageNoContent {
    constructor() {
        this.Errors = [];
        this.StatusCode = 200;
        this.Message = '';
    }

    Errors: string[];
    StatusCode: number;
    Message: string;
}