import { HttpStatusCode } from "@angular/common/http";

export class JoinResponse {
    StatusCode: HttpStatusCode;
    Message: string;
    ConnectionId: string;
}