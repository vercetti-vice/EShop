export class Category {
  id: number;
  name: string;
  parentCategoryId: number;
  parentCategory: Category;
  products: any;
}
