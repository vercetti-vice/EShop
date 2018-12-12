export class Brand {

  constructor(name?: string, description?: string, imageUrl?: string) {
    this.name = name;
    this.description = description;
    this.imageUrl = imageUrl;
  }
  public id: number;
  public name: string;
  public description: string;
  public imageUrl: string;
  public products: any;
}
