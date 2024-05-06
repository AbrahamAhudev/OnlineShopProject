
export class User {


  constructor(
    public Username: string,
    public FirstName: string,
    public LastName: string | null,
    public Password: string,
    public Email: string,
    public Address: string,
    public Phone_Number: number | null
  ) {

  }


}
