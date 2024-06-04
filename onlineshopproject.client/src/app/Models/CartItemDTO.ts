
export class CartItemDTO {


  constructor(
    public cartItemId: number,
    public productId: number,
    public productName: string,
    public productDescription: string,
    public productPrice: number,
    public quantity: number,
    public image: string

  ) {

  }

}
