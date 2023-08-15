import { HttpStatusCode } from '@angular/common/http';

export class LoginRequestModel { 
  constructor(email: string, password: string) {
    this.Email = email;
    this.Password = password;
  }
  Email: string | undefined;
  Password: string | undefined;
}

export class LoginResponseModel {
  constructor() {}
  UserName: string | undefined;
  Token: string | undefined;
  ExpiresIn: number | undefined;
  Rights: string[] | undefined;
  RoleId: number | undefined;
  Message: string | undefined;
  Status: HttpStatusCode | undefined;
  Email: string | undefined;
}
