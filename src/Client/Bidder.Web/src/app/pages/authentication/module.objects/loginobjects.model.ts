export class LoginRequest { 
  constructor(email: string, password: string) {
    this.Email = email;
    this.Password = password;
  }
  Email: string | undefined;
  Password: string | undefined;
}

export class LoginResponse  {
  constructor() {
    this.Token = '';
    this.RefreshToken = '';
    this.Email = '';
    this.UserName = '';
    this.TokenLife = new Date();
  }
  Token : string;
  RefreshToken: string;
  Email: string;
  UserName: string;  
  TokenLife: Date;  
}
