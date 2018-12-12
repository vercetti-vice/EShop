export class Category {

  constructor(name?: string, parentCategoryId?: number) {
    this.name = name;
    this.parentCategoryId = parentCategoryId;
  }
  public id: number;
  public name: string;
  public parentCategoryId: number;
  public parentCategory: Category;
  public products: any;
}
