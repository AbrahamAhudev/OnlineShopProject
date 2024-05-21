
export class User {


  constructor(
    public username: string,
    public firstName: string,
    public lastName: string | null,
    public password: string,
    public email: string,
    public address: string,
    public phone_number: number | null
  ) {

  }


}
