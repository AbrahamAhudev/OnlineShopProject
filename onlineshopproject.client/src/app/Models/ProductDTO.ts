export class ProductDTO {


  constructor(

    public name: string,
    public price: number,
    public description?: string,
    public image?: string,
    public userid?: number

  ) {

  }


}
